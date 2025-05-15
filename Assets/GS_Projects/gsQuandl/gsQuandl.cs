using GpuScript;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using static gsQuandl_;

[TAtt("Quandl|Quandl"), Serializable]
public class gsQuandl : gsQuandl_
{
	public uint txtByte(uint i) => TextByte(csvText, i);
	public bool isAny(uint c, uint a, uint b) => c == a || c == b;
	public override bool AppendBuff_IsBitOn(uint i) => isAny(txtByte(i), ASCII_HT, ASCII_LF);
	public uint2 Get_tab_indexes(uint tabI) => uint2(tabI == 0 ? 0 : AppendBuff_Indexes[tabI - 1] + 1, AppendBuff_Indexes[tabI]);
	public override void csvParseText_GS(uint3 id)
	{
		uint i = id.x, j = (csvHeaderN + i) * csvColN;
		Trade trade = trades[i];
		trade.date = ToUInt(csvText, Get_tab_indexes(j));
		trade.open = ToFloat(csvText, Get_tab_indexes(j + 1));
		trade.high = ToFloat(csvText, Get_tab_indexes(j + 2));
		trade.low = ToFloat(csvText, Get_tab_indexes(j + 3));
		trade.close = ToFloat(csvText, Get_tab_indexes(j + 4));
		trade.volume = ToFloat(csvText, Get_tab_indexes(j + 5));
		trades[i] = trade;
	}

	public string Quandl_SNP_Ticker_File => $"{appPath}Quandl_SNP_Tickers.txt";
	public string[] Get_Quandl_SNP_Tickers()
	{
		csvHeaderN = 1;
		csvColN = 6;
		string qFolder = $"Quandl_2000_2020/", qPath = $"{appPath}{qFolder}/", snpFolder = $"Quandl_SNP_2000_2020/", snpPath = $"{appPath}{snpFolder}/";
		if (Quandl_SNP_Ticker_File.DoesNotExist())
		{
			string[] files = qPath.GetFiles("*.csv");
			var allTickers = files.Select(a => a.BetweenLast("/", ".csv"));
			var snpTickers = $"{appPath}SNP_Tickers.txt".ReadAllLines().ToList();
			var missingTickers = snpTickers.Except(allTickers);
			snpTickers.RemoveAll(a => missingTickers.Contains(a));
			Quandl_SNP_Ticker_File.WriteAllText(snpTickers.Join("\n"));
		}
		var tickers = Quandl_SNP_Ticker_File.ReadAllLines();
		if (snpPath.DoesNotExist()) tickers.Select(f => $"{qPath}{f}.csv").ForEach(f => f.CopyFile(f.Replace(qFolder, snpFolder)));
		return tickers;
	}

	public bool LoadTickerData(string ticker)
	{
		string file = $"{appPath}Quandl_2000_2020/{ticker}.csv";
		if (file.DoesNotExist()) return false;
		ticker_csv_uint_N = AddComputeBufferDataFromFile(ref csvText, file);
		ticker_csv_item_N = AppendBuff_Run(ticker_csv_uint_N * 4);
		ticker_csv_line_N = ticker_csv_item_N / csvColN - csvHeaderN;
		AllocData_trades(tradeN = ticker_csv_line_N);
		Gpu_csvParseText();
		return true;
	}
	public uint year(Trade item) => item.date / 10000;
	public override void Init_yrTradeIs_GS(uint3 id) { uints[UInts_yrTradeI0] = uint_max; uints[UInts_yrTradeI1] = uint_max; }
	public override void Get_yrTradeIs_GS(uint3 id)
	{
		uint i = id.x, j = id.y, n = min(i + startDay, tradeN);
		if (j == 0) { if (i == 0 && year(trades[i]) == trainYear) uints[UInts_yrTradeI0] = n; }
		else if (j == 1) { if (i > 0 && year(trades[i - 1]) == trainYear - 1 && year(trades[i]) == trainYear) uints[UInts_yrTradeI0] = n; }
		else if (j == 2) { if (i == ticker_csv_line_N - 1 && year(trades[i]) == trainYear) uints[UInts_yrTradeI1] = n; }
		else if (j == 3) { if (i > 0 && year(trades[i - 1]) == trainYear && year(trades[i]) == trainYear + 1) uints[UInts_yrTradeI1] = n; }
	}

	public float GetTradeItem(Trade t, uint i) => i == 0 ? t.open : i == 1 ? t.high : i == 2 ? t.low : i == 3 ? t.close : t.volume;
	//public float Get_GainItem(uint trnI0, uint i0, uint trnI1, uint i1) => gain(GetTradeItem(trades[yrTradeRange.x + trnI0], i0), GetTradeItem(trades[yrTradeRange.x + trnI1], i1));
	public float Get_GainItem(uint trnI0, uint i0, uint trnI1, uint i1)
	{
		//float t0 = GetTradeItem(trades[yrTradeRange.x + trnI0], i0), t1 = GetTradeItem(trades[yrTradeRange.x + trnI1], i1), v = gain(t0, t1);
		float t0 = GetTradeItem(trades[trnI0], i0), t1 = GetTradeItem(trades[trnI1], i1), v = gain(t0, t1);
		return v;
	}
	public float Get_ln2GainItem(uint trnI0, uint i0, uint trnI1, uint i1) => ln2(Get_GainItem(trnI0, i0, trnI1, i1) + 1);


	public override float GEM_Lib_Get_trnInput(uint trnI, uint xI)
	{
		trnI += yrTradeRange.x + 1;
		uint i = xI % ETrade_N;
		float v = i == 0 ? Get_ln2GainItem(trnI, ETrade_open, trnI, ETrade_close) : i == 1 ? Get_ln2GainItem(trnI, ETrade_open, trnI, ETrade_high)
			 : i == 2 ? Get_ln2GainItem(trnI, ETrade_low, trnI, ETrade_high) : i == 3 ? Get_ln2GainItem(trnI, ETrade_close, trnI + 1, ETrade_open)
			 : Get_ln2GainItem(trnI - 1, ETrade_volume, trnI, ETrade_volume);
		return v;
	}
	public override float GEM_Lib_Get_trnOutput(uint trnI, uint yI)
	{
		//if (trnI >= 251) //debug
		//	trnI += 0; //debug
		uint day = prevDayN + 1 + startDay;
		float v = Get_ln2GainItem(trnI + day, ETrade_open, trnI + day, yI == 0 ? ETrade_close : ETrade_high);
		return v;
	}
	public override float GEM_Lib_Get_tstInput(uint tstI, uint xI)
	{
		//if (GEM_Lib_tstN == 1)
		//	tstI += yrTradeN + 1 + startDay;
		if (predict == Predict.DayAfterYear)
			tstI += yrTradeN + 1;
		//else if (predict == Predict.SameDay)
		//	tstI += 0;
		return GEM_Lib_Get_trnInput(tstI, xI);
	}
	public override void GEM_Lib__assign_trnInputs_Outputs() => Cpu_GEM_Lib_assign_trn_Inputs_Outputs();
	public override void GEM_Lib__assign_tst_Inputs() => Cpu_GEM_Lib_assign_tst_Inputs();

	//public override void OnValueChanged_GS()
	//{
	//	base.OnValueChanged_GS();
	//	if(UI_trainYear.Changed)
	//}

	//public string[] GetTickers()
	//{
	//	string qFolder = $"Quandl_2000_2020/", qPath = $"{appPath}{qFolder}/", snpFolder = $"Quandl_SNP_2000_2020/", snpPath = $"{appPath}{snpFolder}/";
	//	if (Quandl_SNP_Ticker_File.DoesNotExist())
	//	{
	//		string[] files = qPath.GetFiles("*.csv");
	//		var allTickers = files.Select(a => a.BetweenLast("/", ".csv"));
	//		var snpTickers = $"{appPath}SNP_Tickers.txt".ReadAllLines().ToList();
	//		var missingTickers = snpTickers.Except(allTickers);
	//		snpTickers.RemoveAll(a => missingTickers.Contains(a));
	//		Quandl_SNP_Ticker_File.WriteAllText(snpTickers.Join("\n"));
	//	}
	//	return Quandl_SNP_Ticker_File.ReadAllLines();
	//}
	//{
	//	return new string[] { "A" };
	//}

	public bool UpdateUI()
	{
		//ticker = "";
		//print($"ticker = {ticker}");
		if (!LoadTickerData(ticker)) return false;
		Gpu_Init_yrTradeIs();
		Gpu_Get_yrTradeIs();
		yrTradeRange = uint2(uints[UInts_yrTradeI0], uints[UInts_yrTradeI1]);
		yrTradeN = extent(yrTradeRange);

		startDate = trades[yrTradeRange.x].date.ToString("####/##/##");
		testDate = trades[yrTradeRange.x + prevDayN].date.ToString("####/##/##");
		return true;
	}

	//public void GetTickerProfit(string ticker)
	//{

	//	if (!LoadTickerData(ticker)) return;
	//	StrBldr s = StrBldr().AddTabRow("Date", "Profit");
	//	float profit = 1;
	//	Gpu_Init_yrTradeIs();
	//	Gpu_Get_yrTradeIs();
	//	yrTradeRange = uint2(uints[UInts_yrTradeI0], uints[UInts_yrTradeI1]);
	//	yrTradeN = extent(yrTradeRange);
	//	startDate = trades[yrTradeRange.x + startDay].date.ToString("####/##/##");
	//	testDate = trades[yrTradeRange.x + prevDayN + startDay].date.ToString("####/##/##");

	//	if (predict == Predict.SameDay)
	//	{
	//		GEM_Lib_gpuTrain(inputN: ETrade_N, outputN: 2, exN: 1, Tolerance: tolerance);
	//		GEM_Lib_gpuTest(1);
	//		uint trnI = yrTradeRange.x + prevDayN + startDay;

	//		print($"A {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
	//		print($"A {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");

	//		return;
	//	}

	//	for (startDay = 0; startDay < yrTradeN; startDay++)
	//	{
	//		Gpu_Init_yrTradeIs();
	//		Gpu_Get_yrTradeIs();
	//		yrTradeRange = uint2(uints[UInts_yrTradeI0], uints[UInts_yrTradeI1]);
	//		yrTradeN = extent(yrTradeRange);




	//		if (yrTradeN > prevDayN + 1)
	//		{
	//			GEM_Lib_gpuTrain(inputN: prevDayN * ETrade_N, outputN: 2, exN: yrTradeN, Tolerance: tolerance);
	//			GEM_Lib_gpuTest(yrTradeN);
	//			uint trnI = yrTradeRange.x + prevDayN;


	//			//print($"A {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
	//			//print($"A {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");


	//			//for (uint tstI = 0; tstI < GEM_Lib_tstN; tstI++)
	//			//{
	//			//	uint tradeI = yrTradeRange.x + prevDayN + 1 + tstI;
	//			//	Trade trade = trades[tradeI];
	//			//	float oc_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_close), oh_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_high);
	//			//	float p_oc_gain = ln2gain_to_gain(GEM_Lib_tstYs[tstI]), p_oh_gain = highLevel * ln2gain_to_gain(GEM_Lib_tstYs[tstI + 1]);
	//			//	if (p_oc_gain > minGain) profit *= 1 + (p_oh_gain < oh_gain ? max(oc_gain, p_oh_gain) : oc_gain);
	//			//	s.AddLineTabs(trade.date, profit);
	//			//}
	//			//print($"Profit: {profit}\n{s}");

	//			GEM_Lib_gpuTest(1);
	//			uint tradeI = yrTradeRange.x + yrTradeN + prevDayN + 1 + 1; //?
	//			Trade trade = trades[tradeI];

	//			float oc_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_close), oh_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_high);
	//			float p_oc_gain = ln2gain_to_gain(GEM_Lib_tstYs[0]), p_oh_gain = highLevel * ln2gain_to_gain(GEM_Lib_tstYs[1]);
	//			if (p_oc_gain > minGain) profit *= 1 + (p_oh_gain < oh_gain ? max(oc_gain, p_oh_gain) : oc_gain);
	//			s.AddLineTabs(trade.date, profit);

	//			//trnI = yrTradeRange.x + yrTradeN + prevDayN + 1;
	//			//print($"B {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
	//			//print($"B {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");
	//		}
	//		//break;
	//	}
	//	print($"Profit: {profit}\n{s}");

	//}

	public void print_Trn_Exs()
	{
		//StrBldr s = StrBldr().AddTabRow("ExI", "OC", "OH", "HL", "CO", "VV");
		StrBldr s = StrBldr().AddTabRow("ExI", "Date");
		////for (uint i = 0; i < GEM_Lib_xN; i++) s.Add($"\tX{i + 1}");
		////for (uint i = 0; i < GEM_Lib_yN; i++) s.Add($"\tY{i + 1}");
		//(0, GEM_Lib_xN).ForEach(i => s.Add($"\tX{i + 1}"));
		//(0, GEM_Lib_yN).ForEach(i => s.Add($"\tY{i + 1}"));
		////float v = i == 0 ? Get_ln2GainItem(trnI, ETrade_open, trnI, ETrade_close) : i == 1 ? Get_ln2GainItem(trnI, ETrade_open, trnI, ETrade_high)
		////: i == 2 ? Get_ln2GainItem(trnI, ETrade_low, trnI, ETrade_high) : i == 3 ? Get_ln2GainItem(trnI, ETrade_close, trnI + 1, ETrade_open)
		////: Get_ln2GainItem(trnI - 1, ETrade_volume, trnI, ETrade_volume);

		for (uint trnI = 0; trnI < GEM_Lib_trnN; trnI++)
		{
			//uint i = trnI * tradeN;
			//uint tradeI =
			uint i = yrTradeRange.x + prevDayN * tradeN * trnI + startDay;
			uint tradeI = yrTradeRange.x + prevDayN;
			//s.AddLineTabs(trnI, gain_b(ln2gain_to_gain(GEM_Lib_trnX(trnI, i)), trades[tradeI].open));
			s.AddLineTabs(trnI, trades[i].date);
			//for (uint i = 0; i < GEM_Lib_xN; i++)
			//{
			//	//gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)
			//	s.Add($"\t{gain_b(ln2gain_to_gain(GEM_Lib_trnX(trnI, i)),GetTradeItem())}");
			//}
		}
		print(s);
	}

	public void GetTickerProfit()
	{
		if (!UpdateUI()) return;
		StrBldr s = StrBldr().AddTabRow("Date", "Profit");
		float profit = 1;
		uint startDay0 = startDay;

		if (predict == Predict.SameDay)
		{
			GEM_Lib_gpuTrain(inputN: ETrade_N, outputN: 2, exN: trainDayN, Tolerance: tolerance);
			GEM_Lib_gpuTest(testDayN);
			s.Clear();
			//for (; startDay < testDayN; startDay++)
			for (uint tstI = 0; tstI < testDayN; tstI++, startDay++)
			{
				uint trnI = yrTradeRange.x + prevDayN;
				//uint tradeI = yrTradeRange.x + prevDayN;
				//s.Add(trades[trnI + 1].date, ", Actual open = ", trades[trnI + 1].open, ", Actual close = ", trades[trnI + 1].close,
				//	", Predicted close = ", gain_b(ln2gain_to_gain(GEM_Lib_tstYs[(startDay - startDay0) * 2]), trades[trnI + 1].open),
				//	", Actual high = ", trades[trnI + 1].high,
				//	", Predicted high = ", gain_b(ln2gain_to_gain(GEM_Lib_tstYs[(startDay - startDay0) * 2 + 1]), trades[trnI + 1].open),
				//	"\n");
				s.Add(trades[trnI + 1].date, ", Actual open = ", trades[trnI + 1].open, ", close = ", trades[trnI + 1].close,
					" => ", gain_b(ln2gain_to_gain(GEM_Lib_tstYs[(startDay - startDay0) * 2]), trades[trnI + 1].open),
					", high = ", trades[trnI + 1].high,
					" => ", gain_b(ln2gain_to_gain(GEM_Lib_tstYs[(startDay - startDay0) * 2 + 1]), trades[trnI + 1].open),
					"\n");

				//print($"{trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[(startDay - startDay0) * 2]), trades[trnI + 1].open)}");
				//print($"{trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[(startDay - startDay0) * 2 + 1]), trades[trnI + 1].open)}");
			}
			print(s);
			startDay = startDay0;
			//print_Trn_Exs();
			return;
		}

		for (; startDay < testDayN; startDay++)
		{
			//if (predict == Predict.SameDay)
			//{
			//	//GEM_Lib_gpuTrain(inputN: ETrade_N, outputN: 2, exN: 1, Tolerance: tolerance);
			//	//GEM_Lib_gpuTest(1);
			//	uint trnI = yrTradeRange.x + prevDayN + startDay;
			//	uint tradeI = yrTradeRange.x + prevDayN;
			//	print($"{trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
			//	print($"{trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");
			//}
			//else
			//{
			Gpu_Init_yrTradeIs();
			Gpu_Get_yrTradeIs();
			yrTradeRange = uint2(uints[UInts_yrTradeI0], uints[UInts_yrTradeI1]);
			yrTradeN = extent(yrTradeRange);




			if (yrTradeN > prevDayN + 1)
			{
				GEM_Lib_gpuTrain(inputN: prevDayN * ETrade_N, outputN: 2, exN: yrTradeN, Tolerance: tolerance);
				GEM_Lib_gpuTest(yrTradeN);
				uint trnI = yrTradeRange.x + prevDayN;


				//print($"A {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
				//print($"A {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");


				//for (uint tstI = 0; tstI < GEM_Lib_tstN; tstI++)
				//{
				//	uint tradeI = yrTradeRange.x + prevDayN + 1 + tstI;
				//	Trade trade = trades[tradeI];
				//	float oc_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_close), oh_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_high);
				//	float p_oc_gain = ln2gain_to_gain(GEM_Lib_tstYs[tstI]), p_oh_gain = highLevel * ln2gain_to_gain(GEM_Lib_tstYs[tstI + 1]);
				//	if (p_oc_gain > minGain) profit *= 1 + (p_oh_gain < oh_gain ? max(oc_gain, p_oh_gain) : oc_gain);
				//	s.AddLineTabs(trade.date, profit);
				//}
				//print($"Profit: {profit}\n{s}");

				GEM_Lib_gpuTest(1);
				uint tradeI = yrTradeRange.x + yrTradeN + prevDayN + 1 + 1; //?
				Trade trade = trades[tradeI];

				float oc_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_close), oh_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_high);
				float p_oc_gain = ln2gain_to_gain(GEM_Lib_tstYs[0]), p_oh_gain = highLevel * ln2gain_to_gain(GEM_Lib_tstYs[1]);
				if (p_oc_gain > minGain) profit *= 1 + (p_oh_gain < oh_gain ? max(oc_gain, p_oh_gain) : oc_gain);
				s.AddLineTabs(trade.date, profit);

				//trnI = yrTradeRange.x + yrTradeN + prevDayN + 1;
				//print($"B {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
				//print($"B {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");
			}
			//break;
			//}
		}
		startDay = startDay0;
		print($"Profit: {profit}\n{s}");

	}


	public void RunPredictions(Predict p)
	{
		predict = p;
		InitBuffers();
		string[] snpTickers = Get_Quandl_SNP_Tickers();
		foreach (var tick in snpTickers)
		{
			ticker = tick;
			GetTickerProfit();
			break;
		}
	}

	public override void PredictSameDay() => RunPredictions(Predict.SameDay);
	public override void PredictTrainYear() => RunPredictions(Predict.TrainYear);
	public override void PredictDayAfterYear() => RunPredictions(Predict.DayAfterYear);
	public override void PredictNextYear() => RunPredictions(Predict.NextYear);

	//public override void RunPredictions()
	//{
	//	//runMode = RunMode.Predict;
	//	InitBuffers();
	//	string[] snpTickers = Get_Quandl_SNP_Tickers();
	//	foreach (var tick in snpTickers)
	//	{
	//		GetTickerProfit(tick);
	//		break;
	//	}
	//}

	//public void GetTickerProfit(string ticker)
	//{
	//	LoadTickerData(ticker);
	//	Gpu_Init_yrTradeIs();
	//	Gpu_Get_yrTradeIs();

	//	yrTradeRange = uint2(uints[UInts_yrTradeI0], uints[UInts_yrTradeI1]); //open = 43.96, close = 40.84, p close = 43.81, high = 44, p high = 45.34,
	//																																				//yrTradeRange = uint2(yrTradeRange.x, yrTradeRange.x + 32); //debug: open = 43.96 4 exact, 8 a little high, 16=>42.68, 32=>43.14, ... closer to open
	//	yrTradeN = extent(yrTradeRange);

	//	if (yrTradeN > prevDayN + 1)
	//	{
	//		//GEM_Lib_gpuTrain(inputN: prevDayN * ETrade_N, outputN: 2, exN: yrTradeN, Tolerance: 0.5f);
	//		GEM_Lib_gpuTrain(inputN: prevDayN * ETrade_N, outputN: 2, exN: yrTradeN, Tolerance: tolerance);
	//		GEM_Lib_gpuTest(yrTradeN);
	//		uint trnI = yrTradeRange.x + prevDayN;
	//		print($"A {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
	//		print($"A {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");

	//		StrBldr s = StrBldr().AddTabRow("Date", "Profit");
	//		float profit = 1;
	//		for (uint tstI = 0; tstI < GEM_Lib_tstN; tstI++)
	//		{
	//			uint tradeI = yrTradeRange.x + prevDayN + 1 + tstI;
	//			Trade trade = trades[tradeI];
	//			float oc_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_close);
	//			float oh_gain = Get_GainItem(tradeI, ETrade_open, tradeI, ETrade_high);
	//			float p_oc_gain = ln2gain_to_gain(GEM_Lib_tstYs[tstI]); //T=0.001 => 0.881568, T = 0.5=>1.268058 , T = 0.25=>0.9007879
	//			float p_oh_gain = highLevel * ln2gain_to_gain(GEM_Lib_tstYs[tstI + 1]);
	//			if (p_oc_gain > minGain)
	//			{
	//				if (p_oh_gain < oh_gain) profit *= 1 + max(oc_gain, p_oh_gain);
	//				else profit *= 1 + oc_gain;
	//			}
	//			//if (oc_gain > 0) profit *= 1 + oh_gain;
	//			//if (UnityEngine.Random.value < 0.1f) profit *= 1 + oc_gain;
	//			//profit *= 1 + oh_gain;
	//			s.AddLineTabs(trade.date, profit);
	//		}
	//		print(s);

	//		GEM_Lib_gpuTest(1);
	//		trnI = yrTradeRange.x + yrTradeN + prevDayN + 1;
	//		print($"B {trades[trnI + 1].date}, Actual open = {trades[trnI + 1].open}, Actual close = {trades[trnI + 1].close}, Predicted close = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[0]), trades[trnI + 1].open)}");
	//		print($"B {trades[trnI + 1].date}, Actual high = {trades[trnI + 1].high}, Predicted high = {gain_b(ln2gain_to_gain(GEM_Lib_tstYs[1]), trades[trnI + 1].open)}");
	//	}
	//}

	//public override float GEM_Lib_Get_trnInput(uint trnI, uint xI)
	//{
	//	//if (runMode == RunMode.Test) return trnI;
	//	trnI += yrTradeRange.x + 1;
	//	uint i = xI % ETrade_N;
	//	float v = i == 0 ? Get_ln2GainItem(trnI, ETrade_open, trnI, ETrade_close) : i == 1 ? Get_ln2GainItem(trnI, ETrade_open, trnI, ETrade_high)
	//		 : i == 2 ? Get_ln2GainItem(trnI, ETrade_low, trnI, ETrade_high) : i == 3 ? Get_ln2GainItem(trnI, ETrade_close, trnI + 1, ETrade_open)
	//		 : Get_ln2GainItem(trnI - 1, ETrade_volume, trnI, ETrade_volume);
	//	return v;
	//}
	//public override float GEM_Lib_Get_trnOutput(uint trnI, uint yI)
	//{
	//	//if (runMode == RunMode.Test) return trnI + yI;
	//	return Get_ln2GainItem(trnI + prevDayN + 1, ETrade_open, trnI + prevDayN + 1, yI == 0 ? ETrade_close : ETrade_high);
	//}
	//public override float GEM_Lib_Get_tstInput(uint tstI, uint xI)
	//{
	//	//if (runMode == RunMode.Test) return tstI / (GEM_Lib_tstN - 1.0f);
	//	return GEM_Lib_Get_trnInput(tstI, xI);
	//}

	//public override void Test()
	//{
	//	//runMode = RunMode.Test;
	//	//InitBuffers();
	//	//GEM_Lib_gpuTrain(1, 2, 2, 0.5f);
	//	//GEM_Lib_gpuTest(11);
	//	//print($"gpuTest: ranges = {GEM_Lib_ranges[0]}, {(1, GEM_Lib_tstN * GEM_Lib_yN).For().Select(a => GEM_Lib_tstYs[a].ToString("0.0##")).Join("\t ")}");
	//	//GEM_Lib_Init();
	//	//GEM_Lib_ranges = null;
	//	//var trnExamples = GEM_Lib_NewExampleSet();
	//	//(0.0f, 1.0f, 1.0f).For().ForEach(a => trnExamples = trnExamples.In(a).Out(a, a + 1));
	//	//GEM_Lib_Train(trnExamples, Tolerance: 0.5f);
	//	//print($"trnExamples:  ranges = {GEM_Lib_ranges[0]}, {GEM_Lib_Test((0.0f, 1.0f, 0.1f).For()).Select(a => a.ToString("#,##0.0##")).Join("\t ")}");
	//}
}
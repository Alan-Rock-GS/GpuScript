using GpuScript;
using UnityEngine;

public class gsRand_Doc_GS : _GS
{
	[GS_UI, AttGS("UI|Rand test")] TreeGroup group_UI;

	float f2i;
	[GS_UI, AttGS("Rand|Rand test")] TreeGroup group_Rand;
	[GS_UI, AttGS("N|Number of random numbers", UI.ValRange, 1000, 1, 10000000, UI.Format, "#,###", UI.Pow2_Slider, UI.IsPow10)] uint pntN;

	[GS_UI, AttGS("Avg|Calculate random number average")] TreeGroup group_Avg;
	[GS_UI, AttGS("Calc Avg|Calculate the average")] void Avg() { }
	[GS_UI, AttGS("Avg|Calculated average", UI.ReadOnly)] float Avg_Val;
	[GS_UI, AttGS("Runtime|Time to calculate average", UI.ReadOnly, Unit.ms)] float Avg_Val_Runtime;
	[GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Avg_Val_TFlops;
	[GS_UI, AttGS("Avg|Calculate random number average")] TreeGroupEnd groupEnd_Avg;

	[GS_UI, AttGS("Area π|Calculate PI from area using random numbers")] TreeGroup group_Area_PI;
	[GS_UI, AttGS("Calc Area π|Calculate PI using area of square and circle")] void Area_PI() { }
	[GS_UI, AttGS("Area π|Calculated value of PI using area", UI.ReadOnly)] float Area_PI_Val;
	[GS_UI, AttGS("Area π Error|PI error using area", UI.ReadOnly)] float Area_PI_Error;
	[GS_UI, AttGS("Runtime|Time to calculate PI using area", UI.ReadOnly, Unit.ms)] float Area_PI_Runtime;
	[GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Area_PI_TFlops;
	[GS_UI, AttGS("Area π|Calculate PI using random numbers")] TreeGroupEnd groupEnd_Area_PI;

	[GS_UI, AttGS("Integral π|Calculate PI from integral using random numbers")] TreeGroup group_Integral_PI;
	[GS_UI, AttGS("Calc Integral π|Calculate PI using integral")] void Integral_PI() { }
	[GS_UI, AttGS("Integral π|Calculated value of PI using integral", UI.ReadOnly)] float Integral_PI_Val;
	[GS_UI, AttGS("Integral π Error|PI error using integral", UI.ReadOnly)] float Integral_PI_Error;
	[GS_UI, AttGS("Runtime|Time to calculate PI using integral", UI.ReadOnly, Unit.ms)] float Integral_PI_Runtime;
	[GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Integral_PI_TFlops;
	[GS_UI, AttGS("Integral π|Calculate PI using random numbers")] TreeGroupEnd groupEnd_Integral_PI;

	[GS_UI, AttGS("Rand|Rand test")] TreeGroupEnd groupEnd_Rand;

	uint[] uints { set => Size(1); }
	int[] ints { set => Size(1); }
	void Calc_Average() { Size(pntN); }
	void Count_Pnts_in_Circle() { Size(pntN); }
	void Count_Pnts_out_of_Circle() { Size(pntN); }
	void Integral_Avg() { Size(pntN); }

	[GS_UI] gsRand Rand;
  #region <Rand>
  uint Rand_N, Rand_I, Rand_J;
  uint4 Rand_seed4;
  uint4[] Rand_rs { set => Size(Rand_N); }
  void Rand_initSeed() { Size(Rand_N); }
  void Rand_initState() { Size(Rand_I); }
  [GS_UI, AttGS(GS_Buffer.GroupShared)] uint4[] Rand_grp { set => Size(1024); }
  void Rand_grp_init_1M() { Size(Rand_N / 1024 / 1024); Sync(); }
  void Rand_grp_init_1K() { Size(Rand_N / 1024); Sync(); }
  void Rand_grp_fill_1K() { Size(Rand_N); Sync(); }
  #endregion <Rand>

	[GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>
	#region <OCam_Lib>

	#endregion <OCam_Lib>
	#region <OCam_Lib>

	#endregion <OCam_Lib>

	[GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>
	#region <Report_Lib>

	#endregion <Report_Lib>
	#region <Report_Lib>

	#endregion <Report_Lib>

	[GS_UI, AttGS("UI|Rand test")] TreeGroupEnd group_UI_End;
}
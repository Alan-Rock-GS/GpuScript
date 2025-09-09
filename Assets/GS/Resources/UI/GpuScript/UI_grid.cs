using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_grid : UI_VisualElement
	{
		public bool isExpanded;
		[UxmlAttribute] public bool isClass { get; set; }
		bool inInit = false;
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss)) return false;
			inInit = true;
			uiGrid = gs.root?.Q<UI_grid>(name);
			grid_Container = uiGrid?.Q<VisualElement>("Grid_Container");
			header_Row = grid_Container?.Q<VisualElement>("Header_Row");
			expandButton = header_Row?.Q<Button>("Expand_Button");
			expandButton.RegisterCallback<ClickEvent>(expandButton_Clicked);
			header_Buttons_Container = header_Row?.Q<VisualElement>("Header_Buttons_Container");
			headerButtons = header_Buttons_Container.Query<UI_grid_header>().ToList();
			headerButtons.ForEach(a => a.Init(gs));
			headerButtons.For(h => h.RegisterCallback<ClickEvent>((evt) => OnHeaderButtonClicked(evt)));
			footer_Container = grid_Container?.Q<VisualElement>("Footer_Container");
			addRowButton = footer_Container?.Q<Button>("AddRow_Button");

			addRowButton.UnregisterCallback<ClickEvent>(addRowButton_Clicked);
			addRowButton.RegisterCallback<ClickEvent>(addRowButton_Clicked);

			displayRows = grid_Container?.Query<VisualElement>().Where(a => a.name.StartsWith("Row_")).ToList();
			RowItems = new List<List<UI_VisualElement>>();
			rowNumberButtons = new List<Button>();
			For(displayRows.Count, i =>
			{
				var displayRow = displayRows[i];
				rowNumberButtons.Add(displayRow.Q<Button>("Number_Button"));
				var rowNumber = rowNumberButtons[i];
				rowNumber.RegisterCallback<ClickEvent>((evt) => OnRowNumberButtonClicked(evt));
				rowNumber.RegisterCallback<KeyDownEvent>((evt) => OnRowNumberKeyDown(evt));
				rowNumber.RegisterCallback<KeyUpEvent>((evt) => OnRowNumberKeyUp(evt));
				RowItems.Add(displayRow.Query<UI_VisualElement>().ToList());
				For(RowItems[i].Count, j =>
				{
					var item = RowItems[i][j];
					item.RegisterGridCallbacks(gs, this, i, j);
					if (item is UI_enum) { var e = item as UI_enum; e.SetLabel(true); }
				});
			});

			displayColumns = footer_Container.Query<UI_bool>().ToList();
			displayColumns.For(a => a.toggle.RegisterValueChangedCallback(OnDisplayColumnValueChanged));
			displayColumns.For(a => a.toggle.RegisterCallback<MouseEnterEvent>(OnMouseEnter));
			displayColumns.For(a => a.toggle.RegisterCallback<MouseLeaveEvent>(OnMouseLeave));

			dispRowN = footer_Container.Query<UI_uint>();
			dispRowN.gs = gs;
			dispRowN.textField.RegisterValueChangedCallback(dispRowN_OnTextFieldChanged);// On_dispRowN_Changed);
#if UNITY_STANDALONE_WIN
			dispRowN.textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
#endif //UNITY_STANDALONE_WIN
			dispRowN.textField.RegisterValueChangedCallback(dispRowN.OnValueChanged);
			dispRowN.textField.RegisterCallback<FocusOutEvent>(dispRowN.OnFocusOut);
			dispRowN.textField.RegisterCallback<MouseCaptureEvent>(dispRowN.OnMouseCaptureEvent);
			dispRowN.textField.RegisterCallback<MouseCaptureOutEvent>(dispRowN.OnMouseCaptureOutEvent);
			dispRowN.v = dispRows;
			dispRowN.textField.display(true);
			VScroll = uiGrid?.Q<Scroller>(); VScroll.lowValue = 0; VScroll.highValue = 1000000;
			VScroll.RegisterCallback<ChangeEvent<float>>(OnVScrollChanged);
			VScroll.RegisterCallback<FocusInEvent>(OnSliderFocusIn);
			VScroll.RegisterCallback<FocusOutEvent>(OnSliderFocusOut);
			this.gs ??= gs;
			treeGroup_parent?.ui_children.Add(this);
			inInit = false;
			return true;
		}
		//public UI_grid(string gridName, string gridLabel, string gridDescription, UI_TreeGroup gridParent, int index, int displayRowN, GS gs, Func<int> rowN,
		//	string[] fldNames, object[] ranges, string[] formats = null, bool[] displayCols = null) : base()
		//{
		//	//RowItems = new List<List<UI_VisualElement>>();
		//	this.gs = gs;
		//	RowN = rowN;
		//	name = gridName;
		//	label = gridLabel;
		//	description = gridDescription;
		//	treeGroup_parent = gridParent;
		//	treeGroup_parent_name = gridParent?.name ?? "";
		//	gridParent.ui_children.Add(this);
		//	gs.UI_GS.Insert(index, this);
		//	this.Style(flexGrow: 0, flexDirection: FlexDirection.Row, width: Length.Auto(), flexWrap: Wrap.NoWrap, flexShrink: 1);
		//	dispRowN = new UI_uint() { v = (uint)displayRowN, isGrid = true };
		//	dispRowN.headerLabel.style.display = DisplayStyle.None;

		//	Add(grid_Container = new VisualElement() { name = "Grid_Container" });
		//	grid_Container.style.flexGrow = 0;
		//	grid_Container.Add(header_Row = new VisualElement() { name = "Header_Row" });
		//	header_Row.Style(flexGrow: 0, flexDirection: FlexDirection.Row, height: 20);
		//	header_Row.Add(expandButton = new Button() { name = "Expand_Button", text = "..." });
		//	expandButton.Style(width: 20, unityTextAlign: TextAnchor.MiddleCenter);

		//	header_Row.Add(header_Buttons_Container = new VisualElement() { name = "Header_Buttons_Container" });
		//	header_Buttons_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, display: DisplayStyle.Flex);
		//	headerButtons = new List<UI_grid_header>();
		//	foreach (var fldName in fldNames)
		//	{
		//		var header = new GpuScript.UI_grid_header() { name = $"grid_header_Button_{fldName}" };
		//		header.label = fldName;
		//		header.Style(width: 80, flexGrow: 0, flexDirection: FlexDirection.Column, display: DisplayStyle.Flex);
		//		header_Buttons_Container.Add(header);
		//		headerButtons.Add(header);
		//	}
		//	RowItems = new List<List<UI_VisualElement>>();
		//	displayRows = new List<VisualElement>();
		//	rowNumberButtons = new List<Button>();
		//	for (int i = 0; i < GS.max(DisplayRowN, 30); i++)
		//	{
		//		var row = new VisualElement() { name = $"Row_{i + 1}" };
		//		row.Style(flexDirection: FlexDirection.Row);
		//		var button = new Button() { name = "Number_Button", text = $"{i + 1}" };
		//		button.Style(width: 20, unityTextAlign: TextAnchor.MiddleRight);
		//		row.Add(button);
		//		rowNumberButtons.Add(button);
		//		displayRows.Add(row);
		//		RowItems.Add(new List<UI_VisualElement>());
		//		for (int j = 0; j < fldNames.Length && j < ranges.Length; j++)
		//		{
		//			if (ranges[j] is float2)
		//			{
		//				var r = (float2)ranges[j];
		//				var f = new UI_float() { name = $"UI_float_{fldNames[j]}_{i + 1}", gs = gs, isGrid = true, rangeMin = r.x, rangeMax = r.y, formatString = formats == null || formats[j].IsEmpty() ? "0.000" : formats[j] };
		//				f.headerLabel.style.display = DisplayStyle.None;
		//				f.style.width = f.textField.style.width = 80;
		//				f.siFormat = f.usFormat = f.formatString;
		//				row.Add(f);
		//				RowItems[i].Add(f);
		//			}
		//			else if (ranges[j] is int2)
		//			{
		//				var r = (int2)ranges[j];
		//				var f = new UI_int() { name = $"UI_int_{fldNames[j]}_{i + 1}", gs = gs, isGrid = true, rangeMin = r.x, rangeMax = r.y };
		//				f.headerLabel.style.display = DisplayStyle.None;
		//				f.style.width = f.textField.style.width = 80;
		//				row.Add(f);
		//				RowItems[i].Add(f);
		//			}
		//			else if (ranges[j] is bool2)
		//			{
		//				var f = new UI_bool() { name = $"UI_bool_{fldNames[j]}_{i + 1}", gs = gs, isGrid = true };
		//				f.headerLabel.style.display = DisplayStyle.None;
		//				f.style.width = f.headerLabel.style.width = 20;
		//				row.Add(f);
		//				RowItems[i].Add(f);
		//			}
		//		}
		//		grid_Container.Add(row);
		//	}
		//	footer_Container = new VisualElement() { name = "Footer_Container" };
		//	footer_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, flexWrap: Wrap.Wrap);
		//	var fButton = new Button() { name = "AddRow_Button", text = "+" };
		//	fButton.style.width = 20;
		//	fButton.style.unityTextAlign = TextAnchor.MiddleCenter;
		//	footer_Container.Add(fButton);
		//	displayColumns = new List<UI_bool>();
		//	foreach (var fldName in fldNames)
		//	{
		//		var b = new UI_bool() { v = true, isGrid = true };
		//		b.headerLabel.style.display = DisplayStyle.None;
		//		footer_Container.Add(b);
		//		displayColumns.Add(b);
		//	}
		//	//(0, displayColumns.Count).ForEach(i => displayColumns[i].v = i < displayCols.Length ? displayCols[i] : true);
		//	For(displayColumns.Count, i => displayColumns[i].v = i < displayCols.Length ? displayCols[i] : true);
		//	footer_Container.Add(dispRowN);
		//	grid_Container.Add(footer_Container);
		//	Add(VScroll = new Scroller() { highValue = 100, direction = SliderDirection.Vertical });
		//	Init(gs);
		//}

		/// <summary>
		/// OK: Change Att[], with each Att containing the name, description, valRange, format, unit, readonly, etc
		/// OK: Fix textbox overflow style
		/// OK: Fix dispRowN so the vscrollbar and displayRowN textbox show correctly
		/// Don't call Registration() from UI_Slider_base constructor, call Regristration after assigning all the data from the AttGS[]
		/// </summary>
		public UI_grid(string gridName, string gridLabel, string gridDescription, UI_TreeGroup gridParent, int index, int displayRowN, GS gs, Func<int> rowN,
			(AttGS att, Type uiType)[] attTypes) : base()
		{
			this.gs = gs;
			RowN = rowN;
			name = gridName;
			label = gridLabel;
			description = gridDescription;
			treeGroup_parent = gridParent;
			treeGroup_parent_name = gridParent?.name ?? "";
			gridParent.ui_children.Add(this);
			gs.UI_GS.Insert(index, this);
			this.Style(flexGrow: 0, flexDirection: FlexDirection.Row, width: Length.Auto(), flexWrap: Wrap.NoWrap, flexShrink: 1);
			dispRowN = new UI_uint() { v = (uint)displayRowN, isGrid = true };
			dispRowN.headerLabel.style.display = DisplayStyle.None;

			Add(grid_Container = new VisualElement() { name = "Grid_Container" });
			grid_Container.style.flexGrow = 0;
			grid_Container.Add(header_Row = new VisualElement() { name = "Header_Row" });
			header_Row.Style(flexGrow: 0, flexDirection: FlexDirection.Row, height: 20);
			header_Row.Add(expandButton = new Button() { name = "Expand_Button", text = "..." });
			expandButton.Style(width: 20, unityTextAlign: TextAnchor.MiddleCenter);

			header_Row.Add(header_Buttons_Container = new VisualElement() { name = "Header_Buttons_Container" });
			header_Buttons_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, display: DisplayStyle.Flex);
			headerButtons = new List<UI_grid_header>();
			attTypes.For(typeAtt =>
			{
				var a = typeAtt.att;
				UI_grid_header header = new() { name = $"grid_header_Button_{a.Name}", label = a.Name };
				header.Style(width: 80, flexGrow: 0, flexDirection: FlexDirection.Column, display: DisplayStyle.Flex);
				header_Buttons_Container.Add(header);
				headerButtons.Add(header);
			});

			headerButtons.For(h => h.RegisterCallback<ClickEvent>((evt) => OnHeaderButtonClicked(evt)));
			RowItems = new List<List<UI_VisualElement>>();
			displayRows = new List<VisualElement>();
			rowNumberButtons = new List<Button>();

			For(GS.max(DisplayRowN, 30), i =>
			{
				var row = new VisualElement() { name = $"Row_{i + 1}" };
				row.Style(flexDirection: FlexDirection.Row);
				var rowNumber = new Button() { name = "Number_Button", text = $"{i + 1}" };
				rowNumber.Style(width: 20, unityTextAlign: TextAnchor.MiddleRight);
				row.Add(rowNumber);
				rowNumberButtons.Add(rowNumber);
				rowNumber.RegisterCallback<ClickEvent>((evt) => OnRowNumberButtonClicked(evt));
				rowNumber.RegisterCallback<KeyDownEvent>((evt) => OnRowNumberKeyDown(evt));
				rowNumber.RegisterCallback<KeyUpEvent>((evt) => OnRowNumberKeyUp(evt));
				displayRows.Add(row);
				RowItems.Add(new List<UI_VisualElement>());
				attTypes.For(typeAtt =>
				{
					UI_VisualElement f = typeAtt.uiType.IsAny(typeof(float), typeof(UI_float)) ? new UI_float(i, typeAtt.att) : null;
					if (f != null) { row.Add(f); RowItems[i].Add(f); }
				});
				For(RowItems[i].Count, j =>
				{
					var item = RowItems[i][j];
					item.RegisterGridCallbacks(gs, this, i, j);
					if (item is UI_enum) { var e = item as UI_enum; e.SetLabel(true); }
					else if (item is UI_float) { ((UI_float)item).range = float2(attTypes[j].att.Min, attTypes[j].att.Max); }
				});
				grid_Container.Add(row);
			});
			footer_Container = new VisualElement() { name = "Footer_Container" };
			footer_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, flexWrap: Wrap.Wrap);
			var fButton = new Button() { name = "AddRow_Button", text = "+" };
			fButton.style.width = 20;
			fButton.style.unityTextAlign = TextAnchor.MiddleCenter;
			footer_Container.Add(fButton);
			displayColumns = new List<UI_bool>();
			attTypes.For(typeAtt =>
			{
				var a = typeAtt.att;
				var b = new UI_bool() { v = true, isGrid = true };
				b.headerLabel.style.display = DisplayStyle.None;
				footer_Container.Add(b);
				displayColumns.Add(b);
				b.v = a.ShowIf?.To_bool() ?? true;
			});

			displayColumns.For(a => a.toggle.RegisterValueChangedCallback(OnDisplayColumnValueChanged));
			displayColumns.For(a => a.toggle.RegisterCallback<MouseEnterEvent>(OnMouseEnter));
			displayColumns.For(a => a.toggle.RegisterCallback<MouseLeaveEvent>(OnMouseLeave));
			footer_Container.Add(dispRowN);
			dispRowN.gs = gs;
			dispRowN.textField.RegisterValueChangedCallback(dispRowN_OnTextFieldChanged);// On_dispRowN_Changed);
#if UNITY_STANDALONE_WIN
			dispRowN.textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
#endif //UNITY_STANDALONE_WIN
			dispRowN.textField.RegisterValueChangedCallback(dispRowN.OnValueChanged);
			dispRowN.textField.RegisterCallback<FocusOutEvent>(dispRowN.OnFocusOut);
			dispRowN.textField.RegisterCallback<MouseCaptureEvent>(dispRowN.OnMouseCaptureEvent);
			dispRowN.textField.RegisterCallback<MouseCaptureOutEvent>(dispRowN.OnMouseCaptureOutEvent);
			dispRowN.v = dispRows;
			dispRowN.textField.display(true);

			grid_Container.Add(footer_Container);

			addRowButton = footer_Container?.Q<Button>("AddRow_Button");
			addRowButton.UnregisterCallback<ClickEvent>(addRowButton_Clicked);
			addRowButton.RegisterCallback<ClickEvent>(addRowButton_Clicked);

			Add(VScroll = new Scroller() { highValue = 100, direction = SliderDirection.Vertical });
			VScroll.lowValue = 0; VScroll.highValue = 1000000;
			VScroll.RegisterCallback<ChangeEvent<float>>(OnVScrollChanged);
			VScroll.RegisterCallback<FocusInEvent>(OnSliderFocusIn);
			VScroll.RegisterCallback<FocusOutEvent>(OnSliderFocusOut);
		}

		//		public UI_grid(string gridName, string gridLabel, string gridDescription, UI_TreeGroup gridParent, int index, int displayRowN, GS gs, Func<int> rowN,
		//			(Type uiType, AttGS att)[] typeAtts) : base()
		//		{
		//			this.gs = gs;
		//			RowN = rowN;
		//			name = gridName;
		//			label = gridLabel;
		//			description = gridDescription;
		//			treeGroup_parent = gridParent;
		//			treeGroup_parent_name = gridParent?.name ?? "";
		//			gridParent.ui_children.Add(this);
		//			gs.UI_GS.Insert(index, this);
		//			this.Style(flexGrow: 0, flexDirection: FlexDirection.Row, width: Length.Auto(), flexWrap: Wrap.NoWrap, flexShrink: 1);
		//			dispRowN = new UI_uint() { v = (uint)displayRowN, isGrid = true };
		//			dispRowN.headerLabel.style.display = DisplayStyle.None;

		//			Add(grid_Container = new VisualElement() { name = "Grid_Container" });
		//			grid_Container.style.flexGrow = 0;
		//			grid_Container.Add(header_Row = new VisualElement() { name = "Header_Row" });
		//			header_Row.Style(flexGrow: 0, flexDirection: FlexDirection.Row, height: 20);
		//			header_Row.Add(expandButton = new Button() { name = "Expand_Button", text = "..." });
		//			expandButton.Style(width: 20, unityTextAlign: TextAnchor.MiddleCenter);

		//			header_Row.Add(header_Buttons_Container = new VisualElement() { name = "Header_Buttons_Container" });
		//			header_Buttons_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, display: DisplayStyle.Flex);
		//			headerButtons = new List<UI_grid_header>();
		//			typeAtts.For(typeAtt =>
		//			{
		//				var a = typeAtt.att;
		//				UI_grid_header header = new() { name = $"grid_header_Button_{a.Name}", label = a.Name };
		//				header.Style(width: 80, flexGrow: 0, flexDirection: FlexDirection.Column, display: DisplayStyle.Flex);
		//				header_Buttons_Container.Add(header);
		//				headerButtons.Add(header);
		//			});

		//			headerButtons.For(h => h.RegisterCallback<ClickEvent>((evt) => OnHeaderButtonClicked(evt)));
		//			RowItems = new List<List<UI_VisualElement>>();
		//			displayRows = new List<VisualElement>();
		//			rowNumberButtons = new List<Button>();

		//			For(GS.max(DisplayRowN, 30), i =>
		//			{
		//				var row = new VisualElement() { name = $"Row_{i + 1}" };
		//				row.Style(flexDirection: FlexDirection.Row);
		//				var rowNumber = new Button() { name = "Number_Button", text = $"{i + 1}" };
		//				rowNumber.Style(width: 20, unityTextAlign: TextAnchor.MiddleRight);
		//				row.Add(rowNumber);
		//				rowNumberButtons.Add(rowNumber);
		//				rowNumber.RegisterCallback<ClickEvent>((evt) => OnRowNumberButtonClicked(evt));
		//				rowNumber.RegisterCallback<KeyDownEvent>((evt) => OnRowNumberKeyDown(evt));
		//				rowNumber.RegisterCallback<KeyUpEvent>((evt) => OnRowNumberKeyUp(evt));
		//				displayRows.Add(row);
		//				RowItems.Add(new List<UI_VisualElement>());
		//				typeAtts.For(typeAtt =>
		//				{
		//					UI_VisualElement f = typeAtt.uiType.IsAny(typeof(float), typeof(UI_float)) ? new UI_float(i, typeAtt.att) : null;
		//					if (f != null) { row.Add(f); RowItems[i].Add(f); }
		//				});
		//				For(RowItems[i].Count, j =>
		//				{
		//					var item = RowItems[i][j];
		//					item.RegisterGridCallbacks(gs, this, i, j);
		//					if (item is UI_enum) { var e = item as UI_enum; e.SetLabel(true); }
		//					else if (item is UI_float) { ((UI_float)item).range = float2(typeAtts[j].att.Min, typeAtts[j].att.Max); }
		//				});
		//				grid_Container.Add(row);
		//			});
		//			footer_Container = new VisualElement() { name = "Footer_Container" };
		//			footer_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, flexWrap: Wrap.Wrap);
		//			var fButton = new Button() { name = "AddRow_Button", text = "+" };
		//			fButton.style.width = 20;
		//			fButton.style.unityTextAlign = TextAnchor.MiddleCenter;
		//			footer_Container.Add(fButton);
		//			displayColumns = new List<UI_bool>();
		//			typeAtts.For(typeAtt =>
		//			{
		//				var a = typeAtt.att;
		//				var b = new UI_bool() { v = true, isGrid = true };
		//				b.headerLabel.style.display = DisplayStyle.None;
		//				footer_Container.Add(b);
		//				displayColumns.Add(b);
		//				b.v = a.ShowIf?.To_bool() ?? true;
		//			});

		//			displayColumns.For(a => a.toggle.RegisterValueChangedCallback(OnDisplayColumnValueChanged));
		//			displayColumns.For(a => a.toggle.RegisterCallback<MouseEnterEvent>(OnMouseEnter));
		//			displayColumns.For(a => a.toggle.RegisterCallback<MouseLeaveEvent>(OnMouseLeave));
		//			footer_Container.Add(dispRowN);
		//			dispRowN.gs = gs;
		//			dispRowN.textField.RegisterValueChangedCallback(dispRowN_OnTextFieldChanged);// On_dispRowN_Changed);
		//#if UNITY_STANDALONE_WIN
		//			dispRowN.textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
		//#endif //UNITY_STANDALONE_WIN
		//			dispRowN.textField.RegisterValueChangedCallback(dispRowN.OnValueChanged);
		//			dispRowN.textField.RegisterCallback<FocusOutEvent>(dispRowN.OnFocusOut);
		//			dispRowN.textField.RegisterCallback<MouseCaptureEvent>(dispRowN.OnMouseCaptureEvent);
		//			dispRowN.textField.RegisterCallback<MouseCaptureOutEvent>(dispRowN.OnMouseCaptureOutEvent);
		//			dispRowN.v = dispRows;
		//			dispRowN.textField.display(true);

		//			grid_Container.Add(footer_Container);

		//			addRowButton = footer_Container?.Q<Button>("AddRow_Button");
		//			addRowButton.UnregisterCallback<ClickEvent>(addRowButton_Clicked);
		//			addRowButton.RegisterCallback<ClickEvent>(addRowButton_Clicked);

		//			Add(VScroll = new Scroller() { highValue = 100, direction = SliderDirection.Vertical });
		//			VScroll.lowValue = 0; VScroll.highValue = 1000000;
		//			VScroll.RegisterCallback<ChangeEvent<float>>(OnVScrollChanged);
		//			VScroll.RegisterCallback<FocusInEvent>(OnSliderFocusIn);
		//			VScroll.RegisterCallback<FocusOutEvent>(OnSliderFocusOut);
		//		}

		public UI_grid(string gridName, string gridLabel, string gridDescription, UI_TreeGroup gridParent, int index, int displayRowN, GS gs, Func<int> rowN,
			string[] fldNames, object[] ranges, string[] formats = null, bool[] displayCols = null) : base()
		{
			this.gs = gs;
			RowN = rowN;
			name = gridName;
			label = gridLabel;
			description = gridDescription;
			treeGroup_parent = gridParent;
			treeGroup_parent_name = gridParent?.name ?? "";
			gridParent.ui_children.Add(this);
			gs.UI_GS.Insert(index, this);
			this.Style(flexGrow: 0, flexDirection: FlexDirection.Row, width: Length.Auto(), flexWrap: Wrap.NoWrap, flexShrink: 1);
			dispRowN = new UI_uint() { v = (uint)displayRowN, isGrid = true };
			dispRowN.headerLabel.style.display = DisplayStyle.None;

			Add(grid_Container = new VisualElement() { name = "Grid_Container" });
			grid_Container.style.flexGrow = 0;
			grid_Container.Add(header_Row = new VisualElement() { name = "Header_Row" });
			header_Row.Style(flexGrow: 0, flexDirection: FlexDirection.Row, height: 20);
			header_Row.Add(expandButton = new Button() { name = "Expand_Button", text = "..." });
			expandButton.Style(width: 20, unityTextAlign: TextAnchor.MiddleCenter);

			header_Row.Add(header_Buttons_Container = new VisualElement() { name = "Header_Buttons_Container" });
			header_Buttons_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, display: DisplayStyle.Flex);
			headerButtons = new List<UI_grid_header>();
			foreach (var fldName in fldNames)
			{
				var header = new GpuScript.UI_grid_header() { name = $"grid_header_Button_{fldName}" };
				header.label = fldName;
				header.Style(width: 80, flexGrow: 0, flexDirection: FlexDirection.Column, display: DisplayStyle.Flex);
				header_Buttons_Container.Add(header);
				headerButtons.Add(header);
			}
			headerButtons.For(h => h.RegisterCallback<ClickEvent>((evt) => OnHeaderButtonClicked(evt)));
			RowItems = new List<List<UI_VisualElement>>();
			displayRows = new List<VisualElement>();
			rowNumberButtons = new List<Button>();
			for (int i = 0; i < GS.max(DisplayRowN, 30); i++)
			{
				var row = new VisualElement() { name = $"Row_{i + 1}" };
				row.Style(flexDirection: FlexDirection.Row);
				var rowNumber = new Button() { name = "Number_Button", text = $"{i + 1}" };
				rowNumber.Style(width: 20, unityTextAlign: TextAnchor.MiddleRight);
				row.Add(rowNumber);
				rowNumberButtons.Add(rowNumber);
				rowNumber.RegisterCallback<ClickEvent>((evt) => OnRowNumberButtonClicked(evt));
				rowNumber.RegisterCallback<KeyDownEvent>((evt) => OnRowNumberKeyDown(evt));
				rowNumber.RegisterCallback<KeyUpEvent>((evt) => OnRowNumberKeyUp(evt));
				displayRows.Add(row);
				RowItems.Add(new List<UI_VisualElement>());
				for (int j = 0; j < fldNames.Length && j < ranges.Length; j++)
				{
					if (ranges[j] is float2)
					{
						var r = (float2)ranges[j];
						var f = new UI_float() { name = $"UI_float_{fldNames[j]}_{i + 1}", gs = gs, isGrid = true, rangeMin = r.x, rangeMax = r.y, formatString = formats == null || formats[j].IsEmpty() ? "0.000" : formats[j] };
						f.headerLabel.style.display = DisplayStyle.None;
						f.style.width = f.textField.style.width = 80;
						f.siFormat = f.usFormat = f.formatString;
						style.overflow = Overflow.Hidden;
						row.Add(f);
						RowItems[i].Add(f);
					}
					else if (ranges[j] is int2)
					{
						var r = (int2)ranges[j];
						var f = new UI_int() { name = $"UI_int_{fldNames[j]}_{i + 1}", gs = gs, isGrid = true, rangeMin = r.x, rangeMax = r.y };
						f.headerLabel.style.display = DisplayStyle.None;
						f.style.width = f.textField.style.width = 80;
						row.Add(f);
						RowItems[i].Add(f);
					}
					else if (ranges[j] is bool2)
					{
						var f = new UI_bool() { name = $"UI_bool_{fldNames[j]}_{i + 1}", gs = gs, isGrid = true };
						f.headerLabel.style.display = DisplayStyle.None;
						f.style.width = f.headerLabel.style.width = 20;
						row.Add(f);
						RowItems[i].Add(f);
					}
				}
				for (int j = 0; j < RowItems[i].Count; j++)
				{
					var item = RowItems[i][j];
					item.RegisterGridCallbacks(gs, this, i, j);
					if (item is UI_enum) { var e = item as UI_enum; e.SetLabel(true); }
				}
				grid_Container.Add(row);
			}
			footer_Container = new VisualElement() { name = "Footer_Container" };
			footer_Container.Style(flexGrow: 0, height: 20, flexDirection: FlexDirection.Row, flexWrap: Wrap.Wrap);
			var fButton = new Button() { name = "AddRow_Button", text = "+" };
			fButton.style.width = 20;
			fButton.style.unityTextAlign = TextAnchor.MiddleCenter;
			footer_Container.Add(fButton);
			displayColumns = new List<UI_bool>();
			foreach (var fldName in fldNames)
			{
				var b = new UI_bool() { v = true, isGrid = true };
				b.headerLabel.style.display = DisplayStyle.None;
				footer_Container.Add(b);
				displayColumns.Add(b);
			}
			For(displayColumns.Count, i => displayColumns[i].v = displayCols != null && i < displayCols.Length ? displayCols[i] : true);
			displayColumns.For(a => a.toggle.RegisterValueChangedCallback(OnDisplayColumnValueChanged));
			displayColumns.For(a => a.toggle.RegisterCallback<MouseEnterEvent>(OnMouseEnter));
			displayColumns.For(a => a.toggle.RegisterCallback<MouseLeaveEvent>(OnMouseLeave));
			footer_Container.Add(dispRowN);
			dispRowN.gs = gs;
			dispRowN.textField.RegisterValueChangedCallback(dispRowN_OnTextFieldChanged);// On_dispRowN_Changed);
#if UNITY_STANDALONE_WIN
			dispRowN.textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
#endif //UNITY_STANDALONE_WIN
			dispRowN.textField.RegisterValueChangedCallback(dispRowN.OnValueChanged);
			dispRowN.textField.RegisterCallback<FocusOutEvent>(dispRowN.OnFocusOut);
			dispRowN.textField.RegisterCallback<MouseCaptureEvent>(dispRowN.OnMouseCaptureEvent);
			dispRowN.textField.RegisterCallback<MouseCaptureOutEvent>(dispRowN.OnMouseCaptureOutEvent);
			dispRowN.v = dispRows;
			dispRowN.textField.display(true);

			grid_Container.Add(footer_Container);

			addRowButton = footer_Container?.Q<Button>("AddRow_Button");
			addRowButton.UnregisterCallback<ClickEvent>(addRowButton_Clicked);
			addRowButton.RegisterCallback<ClickEvent>(addRowButton_Clicked);

			Add(VScroll = new Scroller() { highValue = 100, direction = SliderDirection.Vertical });
			VScroll.lowValue = 0; VScroll.highValue = 1000000;
			VScroll.RegisterCallback<ChangeEvent<float>>(OnVScrollChanged);
			VScroll.RegisterCallback<FocusInEvent>(OnSliderFocusIn);
			VScroll.RegisterCallback<FocusOutEvent>(OnSliderFocusOut);
		}
		public UI_grid(string gridName, string gridLabelDescription, UI_TreeGroup gridParent, int index, int displayRowN, GS gs, Func<int> rowN, string fldNamesStr, object[] ranges, string[] formats = null)
			: this(gridName, gridLabelDescription.Before("|"), gridLabelDescription.AfterOrEmpty("|"), gridParent, index, displayRowN, gs, rowN, fldNamesStr.Split("|"), ranges, formats) { }


		public static void UXML(UI_Element e, AttGS att, string name, string label, string typeName, MemberInfo[] _gs_members)
		{
			string fTypeStr = e._GS_memberType.ToString().Before("[]");
			bool isClass = e._GS_memberType.IsClass;
			var classMembers = fTypeStr.GetOrderedMembers();
			uint displayRowN = GS.clamp((uint)att.DisplayRowN, 1, 32), maxRowN = 100;
			float buttonWidth = GS.floor(GS.min(2, GS.log10(maxRowN) + 1)) * 10;
			e.uxml.Add($" style=\"flex-grow: 0; flex-direction: row; width: auto; flex-wrap: nowrap; flex-shrink: 1;\" disp-rows=\"{displayRowN}\" is-class=\"{isClass}\">");
			UXML(e, 1, "<ui:VisualElement name=\"Grid_Container\" style=\"flex-grow: 0;\">");
			{
				UXML(e, 1, "<ui:VisualElement name=\"Header_Row\" style=\"flex-grow: 0; height: 20px; flex-direction: row;\">");
				{
					UXML(e, 1, $"<ui:Button text=\"{e.attGS.Name}...\" name=\"Expand_Button\" style=\"width: 150px; -unity-text-align: middle-center;\" />");
					UXML(e, 0, "<ui:VisualElement name=\"Header_Buttons_Container\" style=\"flex-grow: 0; flex-direction: row; display: none;\">");
					{
						e.uxml_level++;
						uint rowI = 0;
						foreach (var classMember in classMembers)
						{
							var attGS = classMember.AttGS();
							StrBldr units = new StrBldr();
							if (attGS.siUnit != siUnit.Null) units.Add($" si-unit=\"{attGS.siUnit}\"");
							if (attGS.usUnit != usUnit.Null) units.Add($" us-unit=\"{attGS.usUnit}\"");
							if (attGS.Unit != Unit.Null) units.Add($" unit=\"{attGS.Unit}\"");
							UXML(e, 0, $"<GpuScript.UI_grid_header label=\"{attGS.Name}\" name=\"grid_header_Button_{classMember.Name}\" style=\"width: {buttonWidth}px; flex-grow: 0; flex-direction: column; display: flex;\"{units} />");
							rowI++;
						}
					}
					UXML(e, -1, "</ui:VisualElement>");
				}
				UXML(e, -1, "</ui:VisualElement>");

				for (uint rowI = 0; rowI < displayRowN; rowI++)
				{
					UXML(e, 0, $"<ui:VisualElement name=\"Row_{rowI + 1}\" style=\"flex-direction: row; display: none;\">");
					{
						UXML(e, 1, $"<ui:Button tabindex=\"-1\" text=\"{rowI + 1}\" name=\"Number_Button\" style=\"width: {buttonWidth}px; -unity-text-align: middle-right;\" />");
						foreach (var classMember in classMembers)
						{
							Type type = classMember.GetUnderlyingType();
							e._GS_fieldType = type;
							UI_VisualElement.UXML_UI_grid_type_member(e, type, classMember.AttGS(), classMember, rowI, buttonWidth);
						}
					}
					UXML(e, -1, "</ui:VisualElement>");
				}
				UXML(e, 0, $"<ui:VisualElement name=\"Footer_Container\" style=\"flex-grow: 0; height: 20px; flex-direction: row; flex-wrap: wrap; display: none;\">");
				{
					UXML(e, 1, $"<ui:Button tabindex=\"-1\" text=\"+\" name=\"AddRow_Button\" style=\"width: 20px; -unity-text-align: middle-center;\" />");
					for (int i = 0; i < classMembers.Length; i++) UXML(e, 0, $"<GpuScript.UI_bool value=\"true\" is-grid=\"true\" />");
					UXML(e, 0, $"<GpuScript.UI_uint value=\"{displayRowN}\" is-grid=\"true\" />");
				}
				UXML(e, -1, "</ui:VisualElement>");
			}
			UXML(e, -1, "</ui:VisualElement>");
			UXML(e, 0, "<ui:Scroller high-value=\"100\" direction=\"Vertical\" value=\"42\"  style=\"display: none;\"/>");
		}
		public new static void UXML_UI_Element(UI_Element e, MemberInfo[] _gs_members)
		{
			AttGS att = e.attGS;
			string name = e.memberInfo.Name, label = att.Name, typeName = className;
			UI_VisualElement.UXML(e, att, name, label, className);
			UXML(e, att, name, label, className, _gs_members);
			UXML(e, -1, $"</GpuScript.{className}>");
		}
		public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
		{
			if (att == null) return;
			UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
			e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px;\" />");
		}
		public object[][] grid_Data;
		public VisualElement grid_Container, header_Row, footer_Container, header_Buttons_Container;
		public Button expandButton, addRowButton;
		public List<UI_bool> displayColumns;
		public List<UI_grid_header> headerButtons;
		public List<Button> rowNumberButtons;
		public List<VisualElement> displayRows;
		public List<List<UI_VisualElement>> RowItems;
		public Scroller VScroll;
		public UI_grid uiGrid;
		public bool[] isRowSelected;
		[HideInInspector] public string selectedRows;
		[HideInInspector] public int StartRow, lastClickedRow;

		public UI_uint dispRowN;
		public int DisplayRowN => (int)dispRows;
		uint _dispRows = 20;
		[UxmlAttribute] public uint dispRows { get => _dispRows; set { if (dispRowN != null) dispRowN.v = value; _dispRows = value; } }

		public UI_grid() : base() { }
		public void dispRowN_OnTextFieldChanged(ChangeEvent<string> evt)
		{
			dispRows = evt.newValue.To_uint();
			if (DisplayRowN > 20) dispRowN.v = dispRows;
			StartRow = 0;
			if (gs != null)
			{
				$"{name}_OnStartRowChanged".InvokeMethod(gs);
				$"{name}_To_UI".InvokeMethod(gs);
				if (VScroll != null) VScroll.value = 0;
				DrawGrid();
			}
		}
		void OnSliderFocusIn(FocusInEvent evt) => GS.mouseInUI = GS.sliderHasFocus = GS.isGridVScroll = true;
		void OnSliderFocusOut(FocusOutEvent evt) => GS.mouseInUI = GS.sliderHasFocus = GS.isGridVScroll = false;
		public override void OnMouseEnter(MouseEnterEvent evt)
		{
			base.OnMouseEnter(evt);
			var t = evt.currentTarget as Toggle;
			if (gs != null) for (int i = 0; i < displayColumns.Count; i++) if (t == displayColumns[i].toggle) { gs.status = headerButtons[i].label; break; }
		}
		void OnVScrollChanged(ChangeEvent<float> evt)
		{
			int n = arrayLength;
			if (n == 0) return;
			GS.isGridBuilding = true;
			int newStartRow = GS.roundi(VScroll.value / VScroll.highValue * GS.max(0, n - DisplayRowN));
			if (StartRow != newStartRow) { StartRow = newStartRow; $"{name}_OnStartRowChanged".InvokeMethod(gs); $"{name}_To_UI".InvokeMethod(gs); }
			GS.isGridBuilding = false;
		}
		void addRowButton_Clicked(ClickEvent evt)
		{
			$"{name}_OnAddRow".InvokeMethod(gs);
			lastClickedRow = -1;
			selectedRows = "";
			if (arrayLength > DisplayRowN) StartRow = arrayLength - DisplayRowN;
			isRowSelected = (new int[] { arrayLength - 1 }).ints_to_selected_bools(arrayLength);
			DrawGrid();
		}
		uint DisplayColN { get { uint displayN = 0; for (int i = 0; i < displayColumns.Count; i++) displayN += GS.Is(displayColumns[i].v); return displayN; } }
		public static float GetGridWidth(uint colN, bool displayVGrid) => 20 + colN * 100 + (displayVGrid ? 24 : 0);
		private void OnDisplayColumnValueChanged(ChangeEvent<bool> evt) { DrawGrid(); }
		public Func<int> RowN;
		public int arrayLength => RowN == null ? (RowN = $"{name}_GetGridArrayLength".GetMethodExpression<Func<int>>(gs))() : RowN();
		bool Display_VScroll => isExpanded && dispRowN.v < arrayLength;
		public void expandButton_Clicked(ClickEvent evt) { isExpanded = !isExpanded; DrawGrid(); }
		bool inDrawGrid;
		public void DrawGrid()
		{
			if (inDrawGrid) return;
			if (inInit) return;
			if (gs == null) { print($"gs == null, need to assign it"); return; }
			if (gs.in_data_to_ui) return;
			inDrawGrid = true;
			if (isRowSelected?.Length != arrayLength) { lastClickedRow = -1; isRowSelected = (selectedRows = "").RangeStr_to_bools(arrayLength); }
			float numberWidth = GS.max(20, GS.UI_TextWidth("...")), total_w = 0;
			var showHeaderCols = displayColumns.Select((a, i) => (a, i)).Where(a => (bool)($"{name}_ShowIf".InvokeMethod(gs, -1, a.i) ?? true)).Select(a => a.i).ToArray();
			for (int j = 0; RowItems.Count > 0 && j < GS.min(RowItems[0].Count, displayColumns.Count); j++)
			{
				displayColumns[j].display = headerButtons[j].display = false;
				for (int i = 0; i < RowItems.Count; i++) { RowItems[i][j].display = false; if (j == 0) displayRows[i].display(false); }
			}
			foreach (var j in showHeaderCols)
			{
				displayColumns[j].display = true;
				if (displayColumns[j])
				{
					var headerButton = headerButtons[j];
					float w = headerButton.ui_width;
					headerButton.display = true;
					for (int i = 0; i < GS.min(DisplayRowN, RowItems.Count); i++)
					{
						int k;
						int rowI = i + StartRow;
						for (k = j; k < GS.min(RowItems[i].Count, displayColumns.Count) && !displayColumns[k].v && !(bool)($"{name}_ShowIf".InvokeMethod(gs, i, k) ?? true); k++) { }
						if (k < RowItems[i].Count)
						{
							var item = RowItems[i][k];
							item.display = true;
							$"{name}_Assign_BackgroundColor".InvokeMethod(gs, i, k);

							bool displayRow = isExpanded && rowI < arrayLength;
							displayRows[i].display(displayRow);
							if (displayRow)
							{
								w = GS.max(w, item.ui_width + 8);
								var button = rowNumberButtons[i];
								button.text = $"{rowI + 1}";
								if (rowI >= 0 && rowI < (isRowSelected?.Length ?? 0))
									button.style.backgroundColor = isRowSelected?[rowI] ?? false ? new StyleColor(Color.green) : new StyleColor(new Color(194 / 255.0f, 194 / 255.0f, 194 / 255.0f, 255 / 255.0f));
								numberWidth = GS.max(numberWidth, GS.UI_TextWidth(button.text) + 4);
							}
							if ((item as UI_Slider_base)?.sliders[0].style.display == DisplayStyle.Flex) w = GS.max(w, 200);
						}
					}
					headerButton.ui_width = w;
					for (int i = 0, k; i < GS.min(DisplayRowN, RowItems.Count); i++)
					{
						for (k = j; k < GS.min(RowItems[i].Count, displayColumns.Count) && !displayColumns[k].v && !(bool)($"{name}_ShowIf".InvokeMethod(gs, i, k) ?? true); k++) { }
						if (k < RowItems[i].Count)
							RowItems[i][k].ui_width = w;
					}
					total_w += w;
				}
			}
			for (int i = 0; i < GS.min(DisplayRowN, RowItems.Count); i++) rowNumberButtons[i].style.width = numberWidth;
			total_w += numberWidth;
			total_w += Display_VScroll ? 24 : 0;
			header_Buttons_Container.style.display = DisplayIf(isExpanded);
			footer_Container.style.display = DisplayIf(isExpanded);
			dispRowN.v = dispRows;
			if (VScroll != null) VScroll.style.display = DisplayIf(Display_VScroll);
			if (isExpanded)
			{
				float grid_w = GS.max(20 * 2 + 24 + 20 * displayColumns.Count, total_w);
				style.width = grid_w;
				if (uiGrid != null)
				{
					uiGrid.style.width = grid_w;
					uiGrid.grid_Container.style.width = grid_w;
					uiGrid.header_Row.style.width = grid_w;
					uiGrid.footer_Container.style.width = grid_w;
				}
				expandButton.text = "...";
			}
			else { expandButton.text = label + "..."; numberWidth = GS.UI_TextWidth(expandButton.text) + 4; uiGrid.style.width = numberWidth; }
			expandButton.style.width = numberWidth;
			inDrawGrid = false;
		}
		public override void OnUnitsChanged()
		{
			for (int col = 0; col < headerButtons.Count; col++)
			{
				var h = headerButtons[col];
				UI_VisualElement item = RowItems[0][col];
				if (item.siUnit != siUnit.Null || item.usUnit != usUnit.Null)
				{
					h.siUnit = item.siUnit;
					h.usUnit = item.usUnit;
					h.Unit = item.Unit;
					h.OnUnitsChanged();
					for (int row = 0; row < RowItems.Count; row++) RowItems[row][col].OnUnitsChanged();
				}
			}
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public bool isSame(object buff) => true;
		public bool isDifferent(object buff) => !isSame(buff);
		public void OnHeaderButtonClicked(string label) => $"{name}_OnHeaderButtonClicked".InvokeMethod(gs, label);
		public void OnRowNumberButtonClicked(int row)
		{
			if (GS.Ctrl) isRowSelected[row] = !isRowSelected[row];
			else if (GS.Shift)
			{
				if (lastClickedRow >= 0)
				{
					if (lastClickedRow < row) for (int i = lastClickedRow + 1; i <= row && i < isRowSelected.Length; i++) isRowSelected[i] = true;
					else if (lastClickedRow > row) for (int i = row; i <= lastClickedRow && i < isRowSelected.Length; i++) isRowSelected[i] = true;
				}
			}
			else
				for (int i = 0; i < isRowSelected.Length; i++) isRowSelected[i] = row == i;
			lastClickedRow = row;
			DrawGrid();
			$"{name}_OnRowNumberButtonClicked".InvokeMethod(gs, row + 1);
		}
		public void OnRowNumberKeyUp(int row, KeyUpEvent evt) => $"{name}_OnRowNumberKeyUp".InvokeMethod(gs, row, evt);
		public void OnRowNumberKeyDown(int row, KeyDownEvent evt)
		{
			$"{name}_OnRowNumberKeyDown".InvokeMethod(gs, row, evt);
			if (evt.isCut()) $"{name}_OnCut".InvokeMethod(gs);
			else if (evt.keyCode == KeyCode.Insert) $"{name}_OnInsert".InvokeMethod(gs);
			else if (evt.keyCode == KeyCode.Delete)
				$"{name}_OnDelete".InvokeMethod(gs);
			else if (evt.isCopy()) $"{name}_OnCopy".InvokeMethod(gs);
			else if (evt.isPaste()) $"{name}_OnPaste".InvokeMethod(gs);
			else if (evt.isUpArrow()) $"{name}_OnUpArrow".InvokeMethod(gs);
			else if (evt.isDownArrow()) $"{name}_OnDownArrow".InvokeMethod(gs);
			else if (evt.isKey()) $"{name}_OnKeyDown".InvokeMethod(gs);
		}
		void OnHeaderButtonClicked(ClickEvent evt) => OnHeaderButtonClicked(((Button)evt.target).text);
		void OnRowNumberButtonClicked(ClickEvent evt) => rowNumberButtons.Select((b, i) => (b, i)).Where(a => a.b == evt.target).For(a => OnRowNumberButtonClicked(a.i + StartRow));
		void OnRowNumberKeyUp(KeyUpEvent evt) => rowNumberButtons.Select((b, i) => (b, i)).Where(a => a.b == evt.target).For(a => OnRowNumberKeyUp(a.i + StartRow, evt));
		void OnRowNumberKeyDown(KeyDownEvent evt) => rowNumberButtons.Select((b, i) => (b, i)).Where(a => a.b == evt.target).For(a => OnRowNumberKeyDown(a.i + StartRow, evt));
	}
}
Shader "gs/gsGDM"
{
  SubShader
  {
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Off
    Pass
    {
      CGPROGRAM
        #pragma target 5.0
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "../../GS/GS_Shader.cginc"
  #define DataSeparator_Tab	0
  #define DataSeparator_Comma	1
  #define DataSeparator_Constant_Width	2
  #define Fld_IO_Input	0
  #define Fld_IO_Output	1
  #define Fld_IO_None	2
  #define Fld_Val_Type_Float	0
  #define Fld_Val_Type_Int	1
  #define Fld_Val_Type_Bool	2
  #define Fld_Val_Type_String	3
  #define Fld_Val_Type_StringVal	4
  #define Fld_Prediction_Type_None	0
  #define Fld_Prediction_Type_Iterate	1
  #define Fld_Prediction_Type_Constant	2
  #define Fld_Prediction_Type_Constraint	3
  #define OutlierTrust_Inputs	0
  #define OutlierTrust_Outputs	1
  #define OutlierTrust_User_Defined	2
  #define VGrid_Lib_BDraw_Draw_Point	0
  #define VGrid_Lib_BDraw_Draw_Sphere	1
  #define VGrid_Lib_BDraw_Draw_Line	2
  #define VGrid_Lib_BDraw_Draw_Arrow	3
  #define VGrid_Lib_BDraw_Draw_Signal	4
  #define VGrid_Lib_BDraw_Draw_LineSegment	5
  #define VGrid_Lib_BDraw_Draw_Texture_2D	6
  #define VGrid_Lib_BDraw_Draw_Quad	7
  #define VGrid_Lib_BDraw_Draw_WebCam	8
  #define VGrid_Lib_BDraw_Draw_Mesh	9
  #define VGrid_Lib_BDraw_Draw_Number	10
  #define VGrid_Lib_BDraw_Draw_N	11
  #define VGrid_Lib_BDraw_TextAlignment_BottomLeft	0
  #define VGrid_Lib_BDraw_TextAlignment_CenterLeft	1
  #define VGrid_Lib_BDraw_TextAlignment_TopLeft	2
  #define VGrid_Lib_BDraw_TextAlignment_BottomCenter	3
  #define VGrid_Lib_BDraw_TextAlignment_CenterCenter	4
  #define VGrid_Lib_BDraw_TextAlignment_TopCenter	5
  #define VGrid_Lib_BDraw_TextAlignment_BottomRight	6
  #define VGrid_Lib_BDraw_TextAlignment_CenterRight	7
  #define VGrid_Lib_BDraw_TextAlignment_TopRight	8
  #define VGrid_Lib_BDraw_Text_QuadType_FrontOnly	0
  #define VGrid_Lib_BDraw_Text_QuadType_FrontBack	1
  #define VGrid_Lib_BDraw_Text_QuadType_Switch	2
  #define VGrid_Lib_BDraw_Text_QuadType_Arrow	3
  #define VGrid_Lib_BDraw_Text_QuadType_Billboard	4
  #define VGrid_Lib_PaletteType_Rainbow	0
  #define VGrid_Lib_PaletteType_GradientRainbow	1
  #define VGrid_Lib_PaletteType_GradientRainbow10	2
  #define VGrid_Lib_PaletteType_GradientRainbow20	3
  #define VGrid_Lib_PaletteType_Heat	4
  #define VGrid_Lib_PaletteType_GradientHeat	5
  #define VGrid_Lib_PaletteType_WhiteRainbow	6
  #define VGrid_Lib_PaletteType_invRainbow	7
  #define VGrid_Lib_PaletteType_Green	8
  #define VGrid_Lib_PaletteType_Gray	9
  #define VGrid_Lib_PaletteType_DarkGray	10
  #define VGrid_Lib_PaletteType_CT	11
  #define GEM_Lib_BasisType_Pow2	0
  #define GEM_Lib_BasisType_Linear	1
  #define GEM_Lib_BasisType_Cubic	2
  #define GEM_Lib_BasisType_Exp	3
  #define GEM_Lib_UInts_missingInputs	0
  #define GEM_Lib_UInts_missingOutputs	1
  #define GEM_Lib_UInts_best_score	2
  #define GEM_Lib_UInts_best_scoreI	3
  #define GEM_Lib_UInts_mxScore	4
  #define GEM_Lib_UInts_mnScore	5
  #define GEM_Lib_UInts_mxScoreI	6
  #define GEM_Lib_UInts_mnScoreI	7
  #define GEM_Lib_UInts_sum_w	8
  #define GEM_Lib_UInts_maxError	9
  #define GEM_Lib_UInts_maxErrorI	10
  #define GEM_Lib_UInts_maxOutlier	11
  #define GEM_Lib_UInts_maxOutlierI	12
  #define GEM_Lib_UInts_max_fp	13
  #define GEM_Lib_UInts_max_fpI	14
  #define GEM_Lib_UInts_N	15
  #define GEM_Lib_Find_Mode_Unknowns	0
  #define GEM_Lib_Find_Mode_K_U	1
  #define GEM_Lib_Find_Mode_Outliers	2
  #define VGrid_Lib_BDraw_Draw_Text3D 12
  #define VGrid_Lib_BDraw_LF 10
  #define VGrid_Lib_BDraw_TB 9
  #define VGrid_Lib_BDraw_ZERO 48
  #define VGrid_Lib_BDraw_NINE 57
  #define VGrid_Lib_BDraw_PERIOD 46
  #define VGrid_Lib_BDraw_COMMA 44
  #define VGrid_Lib_BDraw_PLUS 43
  #define VGrid_Lib_BDraw_MINUS 45
  #define VGrid_Lib_BDraw_SPACE 32
  #define g gGDM[0]
  #define DataSeparator_Tab	0
  #define DataSeparator_Comma	1
  #define DataSeparator_Constant_Width	2
  #define Fld_IO_Input	0
  #define Fld_IO_Output	1
  #define Fld_IO_None	2
  #define Fld_Val_Type_Float	0
  #define Fld_Val_Type_Int	1
  #define Fld_Val_Type_Bool	2
  #define Fld_Val_Type_String	3
  #define Fld_Val_Type_StringVal	4
  #define Fld_Prediction_Type_None	0
  #define Fld_Prediction_Type_Iterate	1
  #define Fld_Prediction_Type_Constant	2
  #define Fld_Prediction_Type_Constraint	3
  #define OutlierTrust_Inputs	0
  #define OutlierTrust_Outputs	1
  #define OutlierTrust_User_Defined	2
  #define VGrid_Lib_BDraw_Draw_Point	0
  #define VGrid_Lib_BDraw_Draw_Sphere	1
  #define VGrid_Lib_BDraw_Draw_Line	2
  #define VGrid_Lib_BDraw_Draw_Arrow	3
  #define VGrid_Lib_BDraw_Draw_Signal	4
  #define VGrid_Lib_BDraw_Draw_LineSegment	5
  #define VGrid_Lib_BDraw_Draw_Texture_2D	6
  #define VGrid_Lib_BDraw_Draw_Quad	7
  #define VGrid_Lib_BDraw_Draw_WebCam	8
  #define VGrid_Lib_BDraw_Draw_Mesh	9
  #define VGrid_Lib_BDraw_Draw_Number	10
  #define VGrid_Lib_BDraw_Draw_N	11
  #define VGrid_Lib_BDraw_TextAlignment_BottomLeft	0
  #define VGrid_Lib_BDraw_TextAlignment_CenterLeft	1
  #define VGrid_Lib_BDraw_TextAlignment_TopLeft	2
  #define VGrid_Lib_BDraw_TextAlignment_BottomCenter	3
  #define VGrid_Lib_BDraw_TextAlignment_CenterCenter	4
  #define VGrid_Lib_BDraw_TextAlignment_TopCenter	5
  #define VGrid_Lib_BDraw_TextAlignment_BottomRight	6
  #define VGrid_Lib_BDraw_TextAlignment_CenterRight	7
  #define VGrid_Lib_BDraw_TextAlignment_TopRight	8
  #define VGrid_Lib_BDraw_Text_QuadType_FrontOnly	0
  #define VGrid_Lib_BDraw_Text_QuadType_FrontBack	1
  #define VGrid_Lib_BDraw_Text_QuadType_Switch	2
  #define VGrid_Lib_BDraw_Text_QuadType_Arrow	3
  #define VGrid_Lib_BDraw_Text_QuadType_Billboard	4
  #define VGrid_Lib_PaletteType_Rainbow	0
  #define VGrid_Lib_PaletteType_GradientRainbow	1
  #define VGrid_Lib_PaletteType_GradientRainbow10	2
  #define VGrid_Lib_PaletteType_GradientRainbow20	3
  #define VGrid_Lib_PaletteType_Heat	4
  #define VGrid_Lib_PaletteType_GradientHeat	5
  #define VGrid_Lib_PaletteType_WhiteRainbow	6
  #define VGrid_Lib_PaletteType_invRainbow	7
  #define VGrid_Lib_PaletteType_Green	8
  #define VGrid_Lib_PaletteType_Gray	9
  #define VGrid_Lib_PaletteType_DarkGray	10
  #define VGrid_Lib_PaletteType_CT	11
  #define GEM_Lib_BasisType_Pow2	0
  #define GEM_Lib_BasisType_Linear	1
  #define GEM_Lib_BasisType_Cubic	2
  #define GEM_Lib_BasisType_Exp	3
  #define GEM_Lib_UInts_missingInputs	0
  #define GEM_Lib_UInts_missingOutputs	1
  #define GEM_Lib_UInts_best_score	2
  #define GEM_Lib_UInts_best_scoreI	3
  #define GEM_Lib_UInts_mxScore	4
  #define GEM_Lib_UInts_mnScore	5
  #define GEM_Lib_UInts_mxScoreI	6
  #define GEM_Lib_UInts_mnScoreI	7
  #define GEM_Lib_UInts_sum_w	8
  #define GEM_Lib_UInts_maxError	9
  #define GEM_Lib_UInts_maxErrorI	10
  #define GEM_Lib_UInts_maxOutlier	11
  #define GEM_Lib_UInts_maxOutlierI	12
  #define GEM_Lib_UInts_max_fp	13
  #define GEM_Lib_UInts_max_fpI	14
  #define GEM_Lib_UInts_N	15
  #define GEM_Lib_Find_Mode_Unknowns	0
  #define GEM_Lib_Find_Mode_K_U	1
  #define GEM_Lib_Find_Mode_Outliers	2
  #define VGrid_Lib_BDraw_Draw_Text3D 12
  #define VGrid_Lib_BDraw_LF 10
  #define VGrid_Lib_BDraw_TB 9
  #define VGrid_Lib_BDraw_ZERO 48
  #define VGrid_Lib_BDraw_NINE 57
  #define VGrid_Lib_BDraw_PERIOD 46
  #define VGrid_Lib_BDraw_COMMA 44
  #define VGrid_Lib_BDraw_PLUS 43
  #define VGrid_Lib_BDraw_MINUS 45
  #define VGrid_Lib_BDraw_SPACE 32
  struct GGDM
  {
    uint isValidLicense, raw_dataset, processed_dataset, result_dataset, test_results, notepad, excel, visual_studio, dataSeparator, maxPntN, totalExampleN, trnExampleI, trnExampleN, tstExampleI, tstExampleN, featurePntN, thoughtPermutations, thoughtDepth, hasUnknowns, fixUnknowns, fixOutliers, outlierTrust, fixJitter, useFeaturePoints, concurrentLayerN, concurrentNodeN, concurrentLinkN, plotData, drawPoints, draw_IO_Point, draw_Lines_Meshes, plotNeuralNet, animateNet, isNeuralNetActive, NodePntN, NodeLinkI, NodeLinkN, activeNodeN, useInputCorrelations, plotXN, plotYN, predictFuture, predictPast, aiData_rowN, aiData_colN, aiData_inN, aiData_outN, VGrid_Lib_BDraw_AppendBuff_IndexN, VGrid_Lib_BDraw_AppendBuff_BitN, VGrid_Lib_BDraw_AppendBuff_N, VGrid_Lib_BDraw_AppendBuff_BitN1, VGrid_Lib_BDraw_AppendBuff_BitN2, VGrid_Lib_BDraw_omitText, VGrid_Lib_BDraw_includeUnicode, VGrid_Lib_BDraw_fontInfoN, VGrid_Lib_BDraw_textN, VGrid_Lib_BDraw_textCharN, VGrid_Lib_BDraw_boxEdgeN, VGrid_Lib_drawGrid, VGrid_Lib_drawBox, VGrid_Lib_drawAxes, VGrid_Lib_customAxesRangeN, VGrid_Lib_zeroOrigin, VGrid_Lib_drawSurface, VGrid_Lib_GridDrawFront, VGrid_Lib_GridDrawBack, VGrid_Lib_show_slices, VGrid_Lib_paletteType, VGrid_Lib_twoSided, VGrid_Lib_reCalc, VGrid_Lib_buildText, VGrid_Lib_isOrtho, VGrid_Lib_showMeshVal, VGrid_Lib_showMeshRange, VGrid_Lib_showOutline, VGrid_Lib_showSurface, VGrid_Lib_showAxes, VGrid_Lib_showNormalizedAxes, VGrid_Lib_retrace, GEM_Lib_AppendBuff_IndexN, GEM_Lib_AppendBuff_BitN, GEM_Lib_AppendBuff_N, GEM_Lib_AppendBuff_BitN1, GEM_Lib_AppendBuff_BitN2, GEM_Lib_Rand_N, GEM_Lib_Rand_I, GEM_Lib_Rand_J, GEM_Lib_xN, GEM_Lib_yN, GEM_Lib_tstN, GEM_Lib_trnN, GEM_Lib_levelI, GEM_Lib_levelN, GEM_Lib_searchPntN, GEM_Lib_basisType, GEM_Lib_findMode, GEM_Lib_unknownN, GEM_Lib_K_UN, GEM_Lib_outlierN, GEM_Lib_outlier_errorN, GEM_Lib_outlier_y_error_IndexN, GEM_Lib_fpN;
    uint2 cdcYears, VGrid_Lib_viewSize;
    float2 basisRange, link_node_Size, VGrid_Lib_GridX, VGrid_Lib_GridY, VGrid_Lib_GridZ, VGrid_Lib_textSize, VGrid_Lib_paletteRange, VGrid_Lib_meshRange;
    float tolerance, extrapolation, unknownTolerance, outlierTolerance, jitterTolerance, fpTolerance, train_runtime, thought_runtime, evaluation_runtime, pointRadius, IO_pointRadius, neuralNet_Scale, neuralNet_Hollow, predictionMatchImportance, VGrid_Lib_BDraw_fontSize, VGrid_Lib_BDraw_boxThickness, VGrid_Lib_resolution, VGrid_Lib_boxLineThickness, VGrid_Lib_axesOpacity, VGrid_Lib_GridLineThickness, VGrid_Lib_opacity, VGrid_Lib_meshVal, VGrid_Lib_orthoSize, VGrid_Lib_maxDist, VGrid_Lib_minResolution, GEM_Lib_tolerance, GEM_Lib_minBasisWidth, GEM_Lib_maxBasisWidth, GEM_Lib_basisStep, GEM_Lib_basisWidth, GEM_Lib_searchRadius, GEM_Lib_searchStep, GEM_Lib_bestScore, GEM_Lib_bestIterationScore, GEM_Lib_outlier_tolerance, GEM_Lib_fp_tolerance;
    float3 neuralNet_Center, VGrid_Lib_axesRangeMin, VGrid_Lib_axesRangeMax, VGrid_Lib_axesRangeMin1, VGrid_Lib_axesRangeMax1, VGrid_Lib_axesRangeMin2, VGrid_Lib_axesRangeMax2, VGrid_Lib_axesColor, VGrid_Lib_slices, VGrid_Lib_sliceRotation;
    float4 VGrid_Lib_BDraw_boxColor;
    uint3 VGrid_Lib_nodeN;
    uint4 VGrid_Lib_viewRect, GEM_Lib_Rand_seed4;
    Matrix4x4 VGrid_Lib_camToWorld, VGrid_Lib_cameraInvProjection;
  };
  struct VGrid_Lib_BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct VGrid_Lib_BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  struct VGrid_Lib_TRay { float3 origin, direction; float4 color; float dist; };
  struct cdcFld { uint cdcIncludeFld; string cdcFld_Name, cdcFld_NewName, cdcFld_Unit, cdcFld_Desc; uint2 cdcColLen; };
  struct RawFld { string rawFld_Name, rawFld_NewName, rawFld_Unit, rawFld_Desc, rawFld_Conversion; uint rawFld_IO; uint rawFld_Plot, rawFld_Display; uint rawFld_Val_Type; float rawFld_MultiplyFactor; string rawFld_ValidVals, rawFld_Format, rawFld_UnknownVal, rawFld_DefaultVal; float rawFld_DesiredFloat, rawFld_Importance; uint rawFld_Prediction_Type; float rawFld_Prediction_Direction; int rawFld_Monotonic; };
  struct inFld { string fldName, fld_Unit, fldDesc, fld_Conversion; uint fld_Plot; float fld_DesiredFloat, fld_ActualFloat, fld_Importance; uint fld_Type; float2 fld_FloatRange; string fld_Format, fld_UnknownVal; float fld_DefaultVal; uint fld_Prediction_Type; float fld_Prediction_Direction; int fld_Monotonic; };
  struct outFld { string fldName, fld_Unit, fldDesc, fld_Conversion; uint fld_Plot; float fld_DesiredFloat, fld_ActualFloat, fld_Importance; uint fld_Type; float2 fld_FloatRange; string fld_Format, fld_UnknownVal; float fld_DefaultVal; uint fld_Prediction_Type; float fld_Prediction_Direction; int fld_Monotonic; };
  RWStructuredBuffer<GGDM> gGDM;
  RWStructuredBuffer<uint> VGrid_Lib_BDraw_tab_delimeted_text, Links, Link0s, plotXIs, plotYIs, VGrid_Lib_BDraw_AppendBuff_Bits, VGrid_Lib_BDraw_AppendBuff_Sums, VGrid_Lib_BDraw_AppendBuff_Indexes, VGrid_Lib_BDraw_AppendBuff_Fills1, VGrid_Lib_BDraw_AppendBuff_Fills2, GEM_Lib_AppendBuff_Bits, GEM_Lib_AppendBuff_Sums, GEM_Lib_AppendBuff_Indexes, GEM_Lib_AppendBuff_Fills1, GEM_Lib_AppendBuff_Fills2, GEM_Lib_uints, GEM_Lib_scores, GEM_Lib_maxErrors, GEM_Lib_unknowns, GEM_Lib_K_Us, GEM_Lib_outliers, GEM_Lib_outlier_errors, GEM_Lib_fps;
  RWStructuredBuffer<VGrid_Lib_BDraw_TextInfo> VGrid_Lib_BDraw_textInfos;
  RWStructuredBuffer<VGrid_Lib_BDraw_FontInfo> VGrid_Lib_BDraw_fontInfos;
  RWStructuredBuffer<uint2> VGrid_Lib_depthColors;
  RWStructuredBuffer<Color32> VGrid_Lib_paletteBuffer;
  RWStructuredBuffer<uint4> GEM_Lib_Rand_rs;
  RWStructuredBuffer<float3> NodePnts;
  RWStructuredBuffer<float2> activeNodeIRs, GEM_Lib_ranges, GEM_Lib_desiredValWeights;
  RWStructuredBuffer<float> plotVals, actualVals, aiData, aiResults, VGrid_Lib_Vals, GEM_Lib_tstXs, GEM_Lib_tstYs, GEM_Lib_trnXYs, GEM_Lib_trnCs, GEM_Lib_searchPnts, GEM_Lib_bestInputs, GEM_Lib_tstXs0, GEM_Lib_tstYs0, GEM_Lib_trnXs0, GEM_Lib_trnYs0, GEM_Lib_w_avg_Xs;
  RWStructuredBuffer<int> GEM_Lib_ys, GEM_Lib_Ranges, GEM_Lib_wx;

  public Texture2D VGrid_Lib_BDraw_fontTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.plotNeuralNet && g.isValidLicense, g.NodePntN, i, index, LIN); onRenderObject_LIN(g.plotNeuralNet && g.isValidLicense, g.GEM_Lib_xN * g.GEM_Lib_fpN * 5, i, index, LIN); onRenderObject_LIN(g.plotNeuralNet && g.isValidLicense, g.GEM_Lib_yN * g.GEM_Lib_fpN * 5, i, index, LIN); onRenderObject_LIN(g.plotData && g.drawPoints && g.plotXN > 0 && g.plotXN <=3 && g.plotYN > 0 && g.isValidLicense, g.aiData_rowN * g.plotYN, i, index, LIN); onRenderObject_LIN(g.plotData && g.draw_IO_Point && g.plotXN > 0 && g.plotXN <=3 && g.plotYN > 0 && g.isValidLicense, g.plotYN, i, index, LIN); onRenderObject_LIN(g.plotData && g.draw_Lines_Meshes && g.plotXN == 1 && g.plotYN > 0 && g.isValidLicense, g.VGrid_Lib_nodeN.x * g.plotYN, i, index, LIN); onRenderObject_LIN(g.plotData && g.draw_Lines_Meshes && g.plotXN == 2 && g.plotYN > 0 && g.isValidLicense, g.VGrid_Lib_nodeN.x * g.VGrid_Lib_nodeN.y * 2 * g.plotYN, i, index, LIN); onRenderObject_LIN(g.plotData && g.VGrid_Lib_drawBox && g.isValidLicense, g.VGrid_Lib_BDraw_textN + g.GEM_Lib_xN + g.GEM_Lib_yN, i, index, LIN); onRenderObject_LIN(g.plotData && g.VGrid_Lib_drawBox && g.isValidLicense, 12, i, index, LIN); onRenderObject_LIN(g.plotData && g.VGrid_Lib_drawGrid && g.VGrid_Lib_showSurface && g.isValidLicense, product(g.VGrid_Lib_viewSize), i, index, LIN); return LIN; }
  float4 timeColor(float3 p, uint I, inout float r)
  {
    bool isLeft = p.x < g.neuralNet_Center.x;
    if (!g.animateNet) return isLeft ? GREEN : BLUE;
    uint timeN = g.concurrentLayerN * 600;
    float4 color = lerp(YELLOW, isLeft ? GREEN : BLUE, 0.8f * ((_Time.y / 2 * timeN - I) % timeN) / timeN);
    if (g.isNeuralNetActive)
      for (uint i = 0; i < g.activeNodeN; i++)
        if (distance(p, NodePnts[roundu(activeNodeIRs[i].x)] * g.neuralNet_Scale + g.neuralNet_Center) < activeNodeIRs[i].y)
        {
          color = color * 0.6f + RED * 0.4f;
          r *= 1.2f;
          break;
        }
    return color;
  }
  bool loadDataSet = true, search_and_plot = false;
  float4 palette(float v) { return paletteColor(_PaletteTex, v); }
  uint2 GEM_Lib_outlier(uint i) { uint u = GEM_Lib_outliers[i], n = g.GEM_Lib_xN + g.GEM_Lib_yN; return uint2(u / n, u % n); }
  uint2 GEM_Lib_outlier(int i) { return GEM_Lib_outlier((uint)i); }
  uint2 GEM_Lib_unknown(uint i) { uint u = GEM_Lib_unknowns[i], n = g.GEM_Lib_xN + g.GEM_Lib_yN; return uint2(u / n, u % n); }
  uint2 GEM_Lib_unknown(int i) { return GEM_Lib_unknown((uint)i); }
  float lerp_VGrid_Lib_grid(int i, float v) { return lerp(i == 0 ? g.VGrid_Lib_GridX : i == 1 ? g.VGrid_Lib_GridY : g.VGrid_Lib_GridZ, v); }
  VGrid_Lib_BDraw_TextInfo VGrid_Lib_BDraw_textInfo(uint i) { return VGrid_Lib_BDraw_textInfos[i]; }
  float3 VGrid_Lib_gridMin() { return float3(g.VGrid_Lib_GridX.x, g.VGrid_Lib_GridY.x, g.VGrid_Lib_GridZ.x); }
  float3 VGrid_Lib_gridMax() { return float3(g.VGrid_Lib_GridX.y, g.VGrid_Lib_GridY.y, g.VGrid_Lib_GridZ.y); }
  uint2 VGrid_Lib_pixDepthColor(uint i) { return VGrid_Lib_depthColors[i]; }
  uint2 VGrid_Lib_pixDepthColor(uint2 id) { return VGrid_Lib_pixDepthColor(id_to_i(id, g.VGrid_Lib_viewSize)); }
  VGrid_Lib_TRay VGrid_Lib_CreateShaderCameraRay(float2 _uv)
  {
    VGrid_Lib_TRay ray;
    ray.origin = mul(g.VGrid_Lib_camToWorld, g.VGrid_Lib_isOrtho ? float4(g.VGrid_Lib_orthoSize * _uv / float2(aspect(g.VGrid_Lib_viewSize), 1), 0, 1) : f0001).xyz;
    ray.direction = normalize(mul(g.VGrid_Lib_camToWorld, float4(mul(g.VGrid_Lib_cameraInvProjection, float4(_uv, 0, 1)).xyz, 0)).xyz);
    ray.color = f0000;
    ray.dist = 0;
    return ray;
  }
  v2f vert_VGrid_Lib_BDraw_Point(float3 p, float4 color, uint i, v2f o) { o.pos = UnityObjectToClipPos(float4(p, 1)); o.ti = float4(i, 0, VGrid_Lib_BDraw_Draw_Point, 0); o.color = color; return o; }
  float VGrid_Lib_pixDepth(uint2 dc) { return dc.x / (float)uint_max * (g.VGrid_Lib_maxDist - 2); }
  float4 VGrid_Lib_pixColor(uint2 dc) { return c32_f4(u_c32(dc.y)); }
  v2f vert_VGrid_Lib_DrawScreen(uint i, uint j, v2f o)
  {
    uint2 id = i_to_id(i, g.VGrid_Lib_viewSize), dc = VGrid_Lib_pixDepthColor(i);
    float2 uv = (id + f11 * 0.5f) / g.VGrid_Lib_viewSize * 2 - 1;
    VGrid_Lib_TRay ray = VGrid_Lib_CreateShaderCameraRay(uv);
    return vert_VGrid_Lib_BDraw_Point(ray.origin + VGrid_Lib_pixDepth(dc) * ray.direction, VGrid_Lib_pixColor(dc), i, o);
  }
  float4 frag_VGrid_Lib_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  float4 frag_VGrid_Lib_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<VGrid_Lib_BDraw_FontInfo> VGrid_Lib_BDraw_fontInfos, float VGrid_Lib_BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      VGrid_Lib_BDraw_FontInfo f = VGrid_Lib_BDraw_fontInfos[fontInfoI];
      float dx = f.advance / g.VGrid_Lib_BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / g.VGrid_Lib_BDraw_fontSize, mx = float2(f.maxX, f.maxY) / g.VGrid_Lib_BDraw_fontSize, range = mx - mn;
      if (quadType == VGrid_Lib_BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / g.VGrid_Lib_BDraw_fontSize, 0.25f)) / range;
      if (IsNotOutside(uv, f00, f11))
      {
        if (any(f.uvTopLeft > f.uvBottomRight)) uv = uv.yx;
        uv = lerp(f.uvBottomLeft, f.uvTopRight, uv);
        color = tex2Dlod(t, float4(uv, f00));
        color.rgb = (1 - color.rgb) * i.color.rgb;
        if (color.w == 0) color = backColor;
        else color.w = i.color.w;
        break;
      }
      uv_x += dx;
    }
    return color;
  }
  uint2 VGrid_Lib_BDraw_Get_text_indexes(uint textI) { return uint2(textI == 0 ? 0 : VGrid_Lib_BDraw_AppendBuff_Indexes[textI - 1] + 1, textI < g.VGrid_Lib_BDraw_AppendBuff_IndexN ? VGrid_Lib_BDraw_AppendBuff_Indexes[textI] : g.VGrid_Lib_BDraw_textCharN); }
  float aiData_Row_In(uint row, uint inI) { return aiData[row * g.aiData_colN + inI]; }
  float aiData_Row_In(int row, int inI) { return aiData_Row_In((uint)row, (uint)inI); }
  float aiData_Row_In_Grid(uint row, uint inI, int i) { return lerp_VGrid_Lib_grid(i, lerp1(GEM_Lib_ranges[inI], aiData_Row_In(row, inI))); }
  float aiData_Row_In_01(uint row, uint inI) { return lerp1(GEM_Lib_ranges[inI], aiData_Row_In(row, inI)); }
  float aiData_Row_Out(uint row, uint outI) { return aiData[row * g.aiData_colN + g.aiData_inN + outI]; }
  float aiData_Row_Out_01(uint row, uint outI) { return lerp1(GEM_Lib_ranges[g.aiData_inN + outI], aiData_Row_Out(row, outI)); }
  float aiData_Row_Out_Grid(uint row, uint outI) { return lerp_VGrid_Lib_grid((int)g.aiData_inN, lerp1(GEM_Lib_ranges[g.aiData_inN + outI], aiData_Row_Out(row, outI))); }
  float VGrid_Lib_BDraw_wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  uint VGrid_Lib_BDraw_SignalSmpN(uint chI) { return 1024; }
  float VGrid_Lib_BDraw_SignalThickness(uint chI, uint smpI) { return 0.004f; }
  float VGrid_Lib_BDraw_SignalSmpV(uint chI, uint smpI) { return 0; }
  float4 VGrid_Lib_BDraw_SignalColor(uint chI, uint smpI) { return YELLOW; }
  float VGrid_Lib_BDraw_SignalFillCrest(uint chI, uint smpI) { return 1; }
  float4 VGrid_Lib_BDraw_SignalMarker(uint chI, float smpI) { return f0000; }
  float4 VGrid_Lib_BDraw_SignalBackColor(uint chI, uint smpI) { return float4(1, 1, 1, 0.2f); }
  float4 frag_VGrid_Lib_BDraw_Signal(v2f i)
  {
    uint chI = roundu(i.ti.x), SmpN = VGrid_Lib_BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), i.ti.w);
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = VGrid_Lib_BDraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * VGrid_Lib_BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * VGrid_Lib_BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = VGrid_Lib_BDraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(VGrid_Lib_BDraw_SignalSmpV(chI, SmpI), VGrid_Lib_BDraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = VGrid_Lib_BDraw_SignalFillCrest(chI, SmpI);
    float4 marker = VGrid_Lib_BDraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), 1);
    return VGrid_Lib_BDraw_SignalBackColor(chI, SmpI);
  }
  float4 frag_VGrid_Lib_GS(v2f i, float4 color)
  {
    switch (roundu(i.ti.z))
    {
      case uint_max: Discard(0); break;
      case VGrid_Lib_BDraw_Draw_Sphere: color = frag_VGrid_Lib_BDraw_Sphere(i); break;
      case VGrid_Lib_BDraw_Draw_Line: color = frag_VGrid_Lib_BDraw_Line(i); break;
      case VGrid_Lib_BDraw_Draw_Arrow: color = frag_VGrid_Lib_BDraw_Arrow(i); break;
      case VGrid_Lib_BDraw_Draw_Signal: color = frag_VGrid_Lib_BDraw_Signal(i); break;
      case VGrid_Lib_BDraw_Draw_LineSegment: color = frag_VGrid_Lib_BDraw_LineSegment(i); break;
      case VGrid_Lib_BDraw_Draw_Mesh: color = frag_VGrid_Lib_BDraw_Mesh(i); break;
      case VGrid_Lib_BDraw_Draw_Text3D:
        VGrid_Lib_BDraw_TextInfo t = VGrid_Lib_BDraw_textInfo(roundu(i.ti.x));
        color = frag_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_fontTexture, VGrid_Lib_BDraw_tab_delimeted_text, VGrid_Lib_BDraw_fontInfos, g.VGrid_Lib_BDraw_fontSize, t.quadType, t.backColor, VGrid_Lib_BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }
  float4 frag_GS(v2f i, float4 color) { return frag_VGrid_Lib_GS(i, color); }
  uint GEM_Lib_tstYI(uint _tst_PntI, uint yI) { return _tst_PntI * g.GEM_Lib_yN + yI; }
  float GEM_Lib_tstY(uint _tstI, uint yI) { return GEM_Lib_tstYs[GEM_Lib_tstYI(_tstI, yI)]; }
  float GEM_Lib_tstY_v_01(uint _tstI, uint yI) { return lerp1(GEM_Lib_ranges[g.GEM_Lib_xN + yI], GEM_Lib_tstY(_tstI, yI)); }
  uint2 VGrid_Lib_BDraw_JQuadu(uint j) { return uint2(j + 2, j + 1) / 3 % 2; }
  float2 VGrid_Lib_BDraw_JQuadf(uint j) { return (float2)VGrid_Lib_BDraw_JQuadu(j); }
  float4 VGrid_Lib_BDraw_Sphere_quadPoint(float r, uint j) { return r * float4(2 * VGrid_Lib_BDraw_JQuadf(j) - 1, 0, 0); }
  v2f vert_VGrid_Lib_BDraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 p4 = float4(p, 1), quadPoint = VGrid_Lib_BDraw_Sphere_quadPoint(r, j); o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + quadPoint); o.wPos = p; o.uv = quadPoint.xy / r; o.normal = -f001; o.color = color; o.ti = float4(i, 0, VGrid_Lib_BDraw_Draw_Sphere, 0); return o; }
  v2f vert_Nodes(uint i, uint j, v2f o)
  {
    float3 nodePnt = NodePnts[i];
    if (length(nodePnt) < g.neuralNet_Hollow / 2) return o;
    float3 p = nodePnt * g.neuralNet_Scale + g.neuralNet_Center;
    float r = 0.016f * g.neuralNet_Scale * g.link_node_Size.y;
    float4 color = WHITE;
    uint I = i;
    if (I < g.GEM_Lib_xN) color = YELLOW; I -= g.GEM_Lib_xN;
    if (I < g.GEM_Lib_fpN * g.GEM_Lib_levelN) color = timeColor(p, I, r);
    I -= g.GEM_Lib_fpN * g.GEM_Lib_levelN;
    if (I < g.GEM_Lib_yN) color = ORANGE;
    return vert_VGrid_Lib_BDraw_Sphere(p, r, color, i, j, o);
  }
  v2f vert_Draw_Fld_Pnts(uint i, uint j, v2f o)
  {
    if (g.plotYN == 0) return o;
    uint tst_PntI = i / g.plotYN, yI = i % g.plotYN, pI;
    float3 p = f000;
    float4 color = g.plotYN > 0 ? palette(lerp1(g.plotYN - 1, 0, yI)) : RED;
    float r = g.pointRadius / 2;
    if (g.plotXN >= 1) p.x = aiData_Row_In_Grid(tst_PntI, plotXIs[0], 0);
    if (g.plotXN >= 2) p.y = aiData_Row_In_Grid(tst_PntI, plotXIs[1], 1);
    if (g.plotXN >= 3) { p.z = p.y; p.y = aiData_Row_In_01(tst_PntI, plotXIs[2]); if (g.plotYN > 0) color = palette(lerp1(g.VGrid_Lib_paletteRange, aiData_Row_Out_01(tst_PntI, plotYIs[yI]))); }
    if (g.plotXN <= 2 && g.plotYN > 0) { p.z = p.y; p.y = aiData_Row_Out_Grid(tst_PntI, plotYIs[yI]); }
    for (pI = 0; pI < g.GEM_Lib_fpN; pI++) if (GEM_Lib_fps[pI] == tst_PntI) { r *= 2; break; }
    bool found = false;
    for (pI = 0; pI < g.GEM_Lib_outlierN && !found; pI++) found = GEM_Lib_outlier(pI).x == tst_PntI;
    if (found) { color.xyz = (color.xyz + RED.xyz) / 2; if (g.plotXN == 1) p.z -= r / 2; }
    else
    {
      for (pI = 0; pI < g.GEM_Lib_unknownN && !found; pI++) found = GEM_Lib_unknown(pI).x == tst_PntI;
      if (found) { color.xyz = (color.xyz + YELLOW.xyz) / 2; if (g.plotXN == 1) p.z -= r / 2; }
    }
    return vert_VGrid_Lib_BDraw_Sphere(p, r, color, i, j, o);
  }
  v2f vert_Draw_IO_Pnt(uint i, uint j, v2f o)
  {
    uint tst_PntI = i / g.plotYN, yI = i % g.plotYN;
    float3 p = f000;
    float4 color = g.plotYN > 0 ? palette(lerp1(g.plotYN - 1, 0, yI)) : RED;
    float r = g.pointRadius / 2;
    if (g.plotXN >= 1) p.x = actualVals[plotXIs[0]];
    if (g.plotXN >= 2) p.y = actualVals[plotXIs[1]];
    if (g.plotXN >= 3) { p.z = p.y; p.y = actualVals[plotXIs[2]]; if (g.plotYN > 0) color = palette(GEM_Lib_tstY_v_01(tst_PntI, plotYIs[0])); }
    if (g.plotXN <= 2 && g.plotYN > 0) { p.z = p.y; p.y = actualVals[g.GEM_Lib_xN + plotYIs[yI]]; }
    r = g.IO_pointRadius;
    return vert_VGrid_Lib_BDraw_Sphere(p, r, color, i, j, o);
  }
  float2 VGrid_Lib_BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  float4 VGrid_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  v2f vert_VGrid_Lib_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = VGrid_Lib_BDraw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(VGrid_Lib_BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, VGrid_Lib_BDraw_Draw_Line, r); return o; }
  v2f vert_InLinks(uint i, uint j, v2f o)
  {
    float r = 0.002f * g.neuralNet_Scale * g.link_node_Size.x;
    float3 p0 = NodePnts[i % g.GEM_Lib_xN] * g.neuralNet_Scale + g.neuralNet_Center, p1 = NodePnts[i + g.GEM_Lib_xN] * g.neuralNet_Scale + g.neuralNet_Center;
    return vert_VGrid_Lib_BDraw_Line(p0, p1, r, timeColor(p1, i + g.GEM_Lib_xN, r), i, j, o);
  }
  v2f vert_OutLinks(uint i, uint j, v2f o)
  {
    float r = 0.002f * g.neuralNet_Scale * g.link_node_Size.x;
    float3 p0 = NodePnts[g.NodePntN - 1 - (i % g.GEM_Lib_yN)] * g.neuralNet_Scale + g.neuralNet_Center, p1 = NodePnts[g.NodePntN - 1 - (i + g.GEM_Lib_yN)] * g.neuralNet_Scale + g.neuralNet_Center;
    return vert_VGrid_Lib_BDraw_Line(p0, p1, r, timeColor(p1, g.NodePntN - 1 - (i + g.GEM_Lib_yN), r), i, j, o);
  }
  v2f vert_Draw_AI_Lines(uint i, uint j, v2f o)
  {
    uint N = g.VGrid_Lib_nodeN.x, N1 = N - 1, tst_PntI = i % N, yI = i / N;
    if (tst_PntI == N1) return o;
    float r = rcp(N1);
    float3 p0 = float3(tst_PntI * r, plotVals[i], 0), p1 = float3((tst_PntI + 1) * r, plotVals[i + 1], 0);
    float4 color = g.plotYN > 0 ? palette(lerp1(g.plotYN - 1, 0, yI)) : RED;
    return vert_VGrid_Lib_BDraw_Line(p0, p1, g.pointRadius / 4, color, i, j, o);
  }
  v2f vert_Draw_AI_Surfaces(uint i, uint j, v2f o)
  {
    uint3 idN = uint3(g.VGrid_Lib_nodeN.xz, 2 * g.plotYN), id = i_to_id(i, idN);
    bool isX = Is(id.z % 2);
    uint xI = id.x, yI = id.y, plot_yI = id.z / 2, is1 = isX ? 1u : 0;
    uint pntI0 = id_to_i(uint3(xI, yI, plot_yI), idN), pntI1 = id_to_i(uint3(xI + is1, yI + (1 - is1), plot_yI), idN);
    float4 color = g.plotYN > 0 ? palette(lerp1(g.plotYN - 1, 0, plot_yI)) : RED;
    float t = 0.9f, is_t = isX ? t : 0;
    float3 d = f111 / (g.VGrid_Lib_nodeN - 1), p0 = float3(xI, 0, yI) * d, p1 = float3(xI + is_t, 0, yI + (1 - is_t)) * d;
    p0.x = lerp_VGrid_Lib_grid(0, p0.x); p0.z = lerp_VGrid_Lib_grid(2, p0.z);
    p1.x = lerp_VGrid_Lib_grid(0, p1.x); p1.z = lerp_VGrid_Lib_grid(2, p1.z);
    p0.y = plotVals[pntI0]; p1.y = plotVals[pntI1];
    if ((id.x == g.VGrid_Lib_nodeN.x - 1 && isX) || (id.y == g.VGrid_Lib_nodeN.z - 1 && !isX)) return o;
    if (g.plotYN == 1) color = palette(p0.y);
    return vert_VGrid_Lib_BDraw_Line(p0, p1, g.pointRadius / 3, color, i, j, o);
  }
  v2f vert_VGrid_Lib_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_VGrid_Lib_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  v2f vert_VGrid_Lib_BDraw_Box(uint i, uint j, v2f o) { return vert_VGrid_Lib_BDraw_BoxFrame(VGrid_Lib_gridMin(), VGrid_Lib_gridMax(), g.VGrid_Lib_boxLineThickness, DARK_BLUE, i, j, o); }
  float2 VGrid_Lib_BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  v2f vert_VGrid_Lib_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = VGrid_Lib_BDraw_LineArrow_uv(dpf, p0, p1, r, j); o.pos = UnityObjectToClipPos(VGrid_Lib_BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, dpf == 1 ? VGrid_Lib_BDraw_Draw_Line : VGrid_Lib_BDraw_Draw_Arrow, r); return o; }
  v2f vert_VGrid_Lib_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_VGrid_Lib_BDraw_LineArrow(3, p0, p1, r, color, i, j, o); }
  v2f vert_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == VGrid_Lib_BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case VGrid_Lib_BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case VGrid_Lib_BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterCenter: break;
        case VGrid_Lib_BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case VGrid_Lib_BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 p4 = new float4(p, 1), billboardQuad = float4((VGrid_Lib_BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - VGrid_Lib_BDraw_wrapJ(j, 1)) * h, 0, 0);
      o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + billboardQuad + float4(jp, 0));
      o.normal = f00_;
      o.uv = float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize;
      o.ti = float4(i, 0, VGrid_Lib_BDraw_Draw_Text3D, i);
      o.color = color;
    }
    else if (quadType == VGrid_Lib_BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = vert_VGrid_Lib_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o);
      float4 ti = o.ti; ti.z = VGrid_Lib_BDraw_Draw_Text3D; o.ti = ti;
      if (dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
        o.uv = float2(length(q1 - q0) / h * VGrid_Lib_BDraw_wrapJ(j, 1), VGrid_Lib_BDraw_wrapJ(j, 2) - 0.5f);
      else
        o.uv = float2(length(q1 - q0) / h * (1 - VGrid_Lib_BDraw_wrapJ(j, 1)), 0.5f - VGrid_Lib_BDraw_wrapJ(j, 2));
    }
    else if (quadType == VGrid_Lib_BDraw_Text_QuadType_FrontBack || quadType == VGrid_Lib_BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
    {
      bool draw = true;
      if (_WorldSpaceCameraPos.y > p1.y)
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z < p0.z) || (axis == 300 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x > p1.x) || (axis == 2 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 100 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 200 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 4 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 1 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else if (_WorldSpaceCameraPos.y < p0.y)
      {
        if ((axis == 100 && _WorldSpaceCameraPos.z < p0.z) || (axis == 200 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x > p1.x) || (axis == 1 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 400 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 300 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 3 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 2 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z > p0.z) || (axis == 300 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x < p1.x) || (axis == 2 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
        if ((axis == 100 && _WorldSpaceCameraPos.z > p0.z) || (axis == 200 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x < p1.x) || (axis == 1 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
      }
      if (axis == 10 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 10 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      if (draw)
      {
        o.wPos = p;
        switch (justification)
        {
          case VGrid_Lib_BDraw_TextAlignment_BottomLeft: break;
          case VGrid_Lib_BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case VGrid_Lib_BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case VGrid_Lib_BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case VGrid_Lib_BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case VGrid_Lib_BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o.pos = UnityObjectToClipPos(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1);
        o.normal = cross(right, up);
        if (quadType == VGrid_Lib_BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
          o.uv = float2(1 - VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize;
        else
          o.uv = float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize;
        o.ti = float4(i, 0, VGrid_Lib_BDraw_Draw_Text3D, i);
        o.color = color;
      }
    }
    return o;
  }
  v2f vert_VGrid_Lib_BDraw_Text(uint i, uint j, v2f o) { return vert_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_textInfo(i), i, j, o); }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Nodes(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_InLinks(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_OutLinks(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Fld_Pnts(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_IO_Pnt(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_AI_Lines(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_AI_Surfaces(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_BDraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_BDraw_Box(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_DrawScreen(i, j, o); o.tj.x = 0; }
    return o;
  }
  float4 frag(v2f i) : SV_Target
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  v2f vert(uint i : SV_InstanceID, uint j : SV_VertexID)
  {
    v2f o;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
      ENDCG
    }
  }
    Fallback Off
}
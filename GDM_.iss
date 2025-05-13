#define MyAppName "GDM"
#define MyAppVersion "1.000"
#define MyAppPublisher "SummitPeak"
#define MyAppURL "http://www.SummitPeakTechnologies.com/"
#define MyOutputDir "C:/Unity6/GS"

[Setup]
AppId={{ab906c36-5bbd-44b4-a6b7-922e54e3ff72}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commondocs}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir={#MyOutputDir}
OutputBaseFilename={#MyAppName}_setup_
;Password=GS#123
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: {#MyOutputDir}/{#MyAppName} .exe; DestDir: "{app} "; Flags: ignoreversion
Source: {#MyOutputDir}/{#MyAppName} _Data\*; DestDir: "{app} \{#MyAppName} _Data"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: {#MyOutputDir}/MonoBleedingEdge\*; DestDir: "{app} \MonoBleedingEdge"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: {#MyOutputDir}/D3D12\*; DestDir: "{app} \D3D12"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#MyOutputDir}/{#MyAppName}/*"; DestDir: "{app} /{#MyAppName}"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Anscombes/*"; DestDir: "{app} \/GDM/1D Anscombes"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Curve/*"; DestDir: "{app} \/GDM/1D Curve"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Line/*"; DestDir: "{app} \/GDM/1D Line"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Outliers/*"; DestDir: "{app} \/GDM/1D Outliers"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Scatter/*"; DestDir: "{app} \/GDM/1D Scatter"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Test/*"; DestDir: "{app} \/GDM/1D Test"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Cone/*"; DestDir: "{app} \/GDM/2D Cone"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Logic/*"; DestDir: "{app} \/GDM/2D Logic"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/3D Sphere/*"; DestDir: "{app} \/GDM/3D Sphere"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/*"; DestDir: "{app} \/GDM/CDC"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Debug/*"; DestDir: "{app} \/GDM/Debug"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes/*"; DestDir: "{app} \/GDM/Diabetes"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes Prediction/*"; DestDir: "{app} \/GDM/Diabetes Prediction"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs/*"; DestDir: "{app} \/GDM/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GDM/*"; DestDir: "{app} \/GDM/Docs GDM"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GEM/*"; DestDir: "{app} \/GDM/Docs GEM"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GpuScript/*"; DestDir: "{app} \/GDM/Docs GpuScript"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/GDM_SNP_2000_2020/*"; DestDir: "{app} \/GDM/GDM_SNP_2000_2020"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/LLCP/*"; DestDir: "{app} \/GDM/LLCP"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Lung FEV/*"; DestDir: "{app} \/GDM/Lung FEV"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Matrix/*"; DestDir: "{app} \/GDM/Matrix"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Parabola/*"; DestDir: "{app} \/GDM/Parabola"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Quantrade/*"; DestDir: "{app} \/GDM/Quantrade"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Ticker A 2000/*"; DestDir: "{app} \/GDM/Ticker A 2000"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Welding/*"; DestDir: "{app} \/GDM/Welding"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/XOR/*"; DestDir: "{app} \/GDM/XOR"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Anscombes/Docs/*"; DestDir: "{app} \/GDM/1D Anscombes/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Curve/Docs/*"; DestDir: "{app} \/GDM/1D Curve/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Line/Docs/*"; DestDir: "{app} \/GDM/1D Line/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Outliers/Docs/*"; DestDir: "{app} \/GDM/1D Outliers/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Scatter/Docs/*"; DestDir: "{app} \/GDM/1D Scatter/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Test/Docs/*"; DestDir: "{app} \/GDM/1D Test/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Cone/Docs/*"; DestDir: "{app} \/GDM/2D Cone/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Logic/Docs/*"; DestDir: "{app} \/GDM/2D Logic/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/3D Sphere/Docs/*"; DestDir: "{app} \/GDM/3D Sphere/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_1996/*"; DestDir: "{app} \/GDM/CDC/Data_1996"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_1997/*"; DestDir: "{app} \/GDM/CDC/Data_1997"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_1998/*"; DestDir: "{app} \/GDM/CDC/Data_1998"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_1999/*"; DestDir: "{app} \/GDM/CDC/Data_1999"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2000/*"; DestDir: "{app} \/GDM/CDC/Data_2000"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2001/*"; DestDir: "{app} \/GDM/CDC/Data_2001"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2002/*"; DestDir: "{app} \/GDM/CDC/Data_2002"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2003/*"; DestDir: "{app} \/GDM/CDC/Data_2003"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2004/*"; DestDir: "{app} \/GDM/CDC/Data_2004"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2005/*"; DestDir: "{app} \/GDM/CDC/Data_2005"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2006/*"; DestDir: "{app} \/GDM/CDC/Data_2006"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2007/*"; DestDir: "{app} \/GDM/CDC/Data_2007"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2008/*"; DestDir: "{app} \/GDM/CDC/Data_2008"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2009/*"; DestDir: "{app} \/GDM/CDC/Data_2009"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2010/*"; DestDir: "{app} \/GDM/CDC/Data_2010"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2011/*"; DestDir: "{app} \/GDM/CDC/Data_2011"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2012/*"; DestDir: "{app} \/GDM/CDC/Data_2012"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2013/*"; DestDir: "{app} \/GDM/CDC/Data_2013"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2014/*"; DestDir: "{app} \/GDM/CDC/Data_2014"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2015/*"; DestDir: "{app} \/GDM/CDC/Data_2015"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2016/*"; DestDir: "{app} \/GDM/CDC/Data_2016"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2017/*"; DestDir: "{app} \/GDM/CDC/Data_2017"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2018/*"; DestDir: "{app} \/GDM/CDC/Data_2018"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2019/*"; DestDir: "{app} \/GDM/CDC/Data_2019"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2020/*"; DestDir: "{app} \/GDM/CDC/Data_2020"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2021/*"; DestDir: "{app} \/GDM/CDC/Data_2021"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2022/*"; DestDir: "{app} \/GDM/CDC/Data_2022"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/CDC/Data_2023/*"; DestDir: "{app} \/GDM/CDC/Data_2023"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Debug/Docs/*"; DestDir: "{app} \/GDM/Debug/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes/Docs/*"; DestDir: "{app} \/GDM/Diabetes/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes Prediction/Docs/*"; DestDir: "{app} \/GDM/Diabetes Prediction/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GDM/Docs/*"; DestDir: "{app} \/GDM/Docs GDM/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GEM/Docs/*"; DestDir: "{app} \/GDM/Docs GEM/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GpuScript/Docs/*"; DestDir: "{app} \/GDM/Docs GpuScript/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Lung FEV/Docs/*"; DestDir: "{app} \/GDM/Lung FEV/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Matrix/Docs/*"; DestDir: "{app} \/GDM/Matrix/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Parabola/Docs/*"; DestDir: "{app} \/GDM/Parabola/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Welding/Docs/*"; DestDir: "{app} \/GDM/Welding/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/XOR/Docs/*"; DestDir: "{app} \/GDM/XOR/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Anscombes/Docs/HTML/*"; DestDir: "{app} \/GDM/1D Anscombes/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Curve/Docs/HTML/*"; DestDir: "{app} \/GDM/1D Curve/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Line/Docs/HTML/*"; DestDir: "{app} \/GDM/1D Line/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Outliers/Docs/HTML/*"; DestDir: "{app} \/GDM/1D Outliers/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Scatter/Docs/HTML/*"; DestDir: "{app} \/GDM/1D Scatter/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Test/Docs/HTML/*"; DestDir: "{app} \/GDM/1D Test/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Cone/Docs/HTML/*"; DestDir: "{app} \/GDM/2D Cone/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Logic/Docs/HTML/*"; DestDir: "{app} \/GDM/2D Logic/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/3D Sphere/Docs/HTML/*"; DestDir: "{app} \/GDM/3D Sphere/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes/Docs/HTML/*"; DestDir: "{app} \/GDM/Diabetes/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GDM/Docs/HTML/*"; DestDir: "{app} \/GDM/Docs GDM/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GEM/Docs/HTML/*"; DestDir: "{app} \/GDM/Docs GEM/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GpuScript/Docs/HTML/*"; DestDir: "{app} \/GDM/Docs GpuScript/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Lung FEV/Docs/HTML/*"; DestDir: "{app} \/GDM/Lung FEV/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Matrix/Docs/HTML/*"; DestDir: "{app} \/GDM/Matrix/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Parabola/Docs/HTML/*"; DestDir: "{app} \/GDM/Parabola/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Welding/Docs/HTML/*"; DestDir: "{app} \/GDM/Welding/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/XOR/Docs/HTML/*"; DestDir: "{app} \/GDM/XOR/Docs/HTML"; Flags: ignoreversion
Source: {#MyOutputDir}/UnityPlayer.dll; DestDir: "{app} "; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app} \{#MyAppName} .exe"
Name: "{group}\{cm:UninstallProgram,{#MyAppName} \}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app} \{#MyAppName} .exe"; Tasks: desktopicon

[Run]
Filename: "{app} \{#MyAppName} .exe"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
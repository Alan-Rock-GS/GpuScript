#define MyAppName "GDM"
#define MyAppVersion "1.000"
#define MyAppPublisher "SummitPeak"
#define MyAppURL "http://www.SummitPeakTechnologies.com/"
#define MyOutputDir "D:/Unity6/GS"

[Setup]
AppId={{3c64a967-13e4-4407-bf91-9719aab61445}
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
OutputBaseFilename={#MyAppName}_setup_1.0
;Password=GS#123
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: {#MyOutputDir}/{#MyAppName} 1.0.exe; DestDir: "{app} 1.0"; Flags: ignoreversion
Source: {#MyOutputDir}/{#MyAppName} 1.0_Data\*; DestDir: "{app} 1.0\{#MyAppName} 1.0_Data"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: {#MyOutputDir}/MonoBleedingEdge\*; DestDir: "{app} 1.0\MonoBleedingEdge"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: {#MyOutputDir}/D3D12\*; DestDir: "{app} 1.0\D3D12"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#MyOutputDir}/{#MyAppName}/*"; DestDir: "{app} 1.0/{#MyAppName}"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Anscombes/*"; DestDir: "{app} 1.0\/GDM/1D Anscombes"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Curve/*"; DestDir: "{app} 1.0\/GDM/1D Curve"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Line/*"; DestDir: "{app} 1.0\/GDM/1D Line"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Outliers/*"; DestDir: "{app} 1.0\/GDM/1D Outliers"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Scatter/*"; DestDir: "{app} 1.0\/GDM/1D Scatter"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Test/*"; DestDir: "{app} 1.0\/GDM/1D Test"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Cone/*"; DestDir: "{app} 1.0\/GDM/2D Cone"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Logic/*"; DestDir: "{app} 1.0\/GDM/2D Logic"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/3D Sphere/*"; DestDir: "{app} 1.0\/GDM/3D Sphere"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Debug/*"; DestDir: "{app} 1.0\/GDM/Debug"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes/*"; DestDir: "{app} 1.0\/GDM/Diabetes"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes Prediction/*"; DestDir: "{app} 1.0\/GDM/Diabetes Prediction"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GDM/*"; DestDir: "{app} 1.0\/GDM/Docs GDM"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GEM/*"; DestDir: "{app} 1.0\/GDM/Docs GEM"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GpuScript/*"; DestDir: "{app} 1.0\/GDM/Docs GpuScript"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Lung FEV/*"; DestDir: "{app} 1.0\/GDM/Lung FEV"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Matrix/*"; DestDir: "{app} 1.0\/GDM/Matrix"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Parabola/*"; DestDir: "{app} 1.0\/GDM/Parabola"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Welding/*"; DestDir: "{app} 1.0\/GDM/Welding"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Anscombes/Docs/*"; DestDir: "{app} 1.0\/GDM/1D Anscombes/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Curve/Docs/*"; DestDir: "{app} 1.0\/GDM/1D Curve/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Line/Docs/*"; DestDir: "{app} 1.0\/GDM/1D Line/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Outliers/Docs/*"; DestDir: "{app} 1.0\/GDM/1D Outliers/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Scatter/Docs/*"; DestDir: "{app} 1.0\/GDM/1D Scatter/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Test/Docs/*"; DestDir: "{app} 1.0\/GDM/1D Test/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Cone/Docs/*"; DestDir: "{app} 1.0\/GDM/2D Cone/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Logic/Docs/*"; DestDir: "{app} 1.0\/GDM/2D Logic/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/3D Sphere/Docs/*"; DestDir: "{app} 1.0\/GDM/3D Sphere/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Debug/Docs/*"; DestDir: "{app} 1.0\/GDM/Debug/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes/Docs/*"; DestDir: "{app} 1.0\/GDM/Diabetes/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GDM/Docs/*"; DestDir: "{app} 1.0\/GDM/Docs GDM/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GEM/Docs/*"; DestDir: "{app} 1.0\/GDM/Docs GEM/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GpuScript/Docs/*"; DestDir: "{app} 1.0\/GDM/Docs GpuScript/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Lung FEV/Docs/*"; DestDir: "{app} 1.0\/GDM/Lung FEV/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Matrix/Docs/*"; DestDir: "{app} 1.0\/GDM/Matrix/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Welding/Docs/*"; DestDir: "{app} 1.0\/GDM/Welding/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Anscombes/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/1D Anscombes/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Curve/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/1D Curve/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Line/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/1D Line/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Outliers/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/1D Outliers/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Scatter/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/1D Scatter/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/1D Test/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/1D Test/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Cone/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/2D Cone/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/2D Logic/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/2D Logic/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/3D Sphere/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/3D Sphere/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Diabetes/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Diabetes/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GDM/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Docs GDM/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GEM/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Docs GEM/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Docs GpuScript/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Docs GpuScript/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Lung FEV/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Lung FEV/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Matrix/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Matrix/Docs/HTML"; Flags: ignoreversion
Source: "{#MyOutputDir}/GDM/Welding/Docs/HTML/*"; DestDir: "{app} 1.0\/GDM/Welding/Docs/HTML"; Flags: ignoreversion
Source: {#MyOutputDir}/UnityPlayer.dll; DestDir: "{app} 1.0"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app} 1.0\{#MyAppName} 1.0.exe"
Name: "{group}\{cm:UninstallProgram,{#MyAppName} 1.0\}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app} 1.0\{#MyAppName} 1.0.exe"; Tasks: desktopicon

[Run]
Filename: "{app} 1.0\{#MyAppName} 1.0.exe"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
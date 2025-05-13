#define MyAppName "Fractal"
#define MyAppVersion "1.000"
#define MyAppPublisher "SummitPeak"
#define MyAppURL "http://www.SummitPeakTechnologies.com/"
#define MyOutputDir "C:/Unity6/GS"

[Setup]
AppId={{ee3f310e-92ec-4d73-a3ec-dfb43e7b35e4}
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
Source: "{#MyOutputDir}/Fractal/Docs/*"; DestDir: "{app} \/Fractal/Docs"; Flags: ignoreversion
Source: "{#MyOutputDir}/Fractal/Docs/HTML/*"; DestDir: "{app} \/Fractal/Docs/HTML"; Flags: ignoreversion
Source: {#MyOutputDir}/UnityPlayer.dll; DestDir: "{app} "; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app} \{#MyAppName} .exe"
Name: "{group}\{cm:UninstallProgram,{#MyAppName} \}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app} \{#MyAppName} .exe"; Tasks: desktopicon

[Run]
Filename: "{app} \{#MyAppName} .exe"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
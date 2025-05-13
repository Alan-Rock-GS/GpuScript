#define MyAppName "TClient"
#define MyAppVersion "1.000"
#define MyAppPublisher "ThunderTech"
#define MyAppURL "http://www.ThunderTech-inc.com/"
#define MyOutputDir "D:/Unity 2023/GS10"

[Setup]
AppId={{c061a5c0-df19-4e3a-bea5-6ad29cef6648}
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
Source: "{#MyOutputDir}/TClient/3D FD/*"; DestDir: "{app} 1.0\/TClient/3D FD"; Flags: ignoreversion
Source: "{#MyOutputDir}/TClient/PluSolver-master/*"; DestDir: "{app} 1.0\/TClient/PluSolver-master"; Flags: ignoreversion
Source: "{#MyOutputDir}/TClient/Report/*"; DestDir: "{app} 1.0\/TClient/Report"; Flags: ignoreversion
Source: {#MyOutputDir}/UnityPlayer.dll; DestDir: "{app} 1.0"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app} 1.0\{#MyAppName} 1.0.exe"
Name: "{group}\{cm:UninstallProgram,{#MyAppName} 1.0\}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app} 1.0\{#MyAppName} 1.0.exe"; Tasks: desktopicon

[Run]
Filename: "{app} 1.0\{#MyAppName} 1.0.exe"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
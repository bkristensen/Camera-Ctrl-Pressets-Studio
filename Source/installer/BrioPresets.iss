; Build from Windows with scripts/build-installer.ps1.
; This installer is unsigned until a publisher signing process is configured.
#ifndef AppVersion
  #define AppVersion "0.6.1-beta.1"
#endif
#define RepoRoot AddBackslash(SourcePath) + ".."

[Setup]
AppId={{B9D05FC2-137E-4AA9-B006-7013227FCDB5}
AppName=Cam Pressets Studio
AppVersion={#AppVersion}
AppPublisher=Cam Pressets contributors
DefaultDirName={localappdata}\Programs\Cam Pressets
DefaultGroupName=Cam Pressets
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
MinVersion=10.0.17763
OutputDir={#RepoRoot}\artifacts\installers
OutputBaseFilename=Cam-Pressets-Studio-Setup-{#AppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
SetupIconFile={#RepoRoot}\BrioPresets\Assets\CamPressetsStudio.ico
LanguageDetectionMethod=uilanguage
ShowLanguageDialog=auto
LicenseFile={#RepoRoot}\LICENSE
CloseApplications=yes
RestartApplications=no
UninstallDisplayIcon={app}\Cam Pressets Studio.exe

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "danish"; MessagesFile: "compiler:Languages\Danish.isl"

[CustomMessages]
english.NetRequired=Cam Pressets requires .NET Framework 4.8. Install it from Microsoft, then run this installer again.
danish.NetRequired=Cam Pressets kræver .NET Framework 4.8. Installér det fra Microsoft, og kør derefter installationsprogrammet igen.

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#RepoRoot}\BrioPresets\bin\Release\net48\Cam Pressets Studio.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\BrioPresets\bin\Release\net48\Cam Pressets Studio.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\README.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Cam Pressets Studio"; Filename: "{app}\Cam Pressets Studio.exe"
Name: "{autodesktop}\Cam Pressets Studio"; Filename: "{app}\Cam Pressets Studio.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Cam Pressets Studio.exe"; Description: "{cm:LaunchProgram,Cam Pressets Studio}"; Flags: nowait postinstall skipifsilent

[Code]
function InitializeSetup(): Boolean;
var
  Release: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM32, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release);
  if Result then
    Result := Release >= 528040;
  if not Result then
    MsgBox(CustomMessage('NetRequired'), mbError, MB_OK);
end;

; Build from Windows with scripts/build-installer.ps1.
; This installer is unsigned until a publisher signing process is configured.
#ifndef AppVersion
  #define AppVersion "0.7.0-beta.3"
#endif
#define RepoRoot AddBackslash(SourcePath) + ".."

[Setup]
AppId={{B9D05FC2-137E-4AA9-B006-7013227FCDB5}
AppName=Camera Control Studio
AppVersion={#AppVersion}
AppPublisher=Brian Kristensen
AppPublisherURL=https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio
AppSupportURL=https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues
AppUpdatesURL=https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases
VersionInfoCompany=Brian Kristensen
VersionInfoCopyright=Copyright (c) 2026 Cam Pressets contributors
DefaultDirName={localappdata}\Programs\CamCtrlStudio
DefaultGroupName=Camera Control Studio
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
MinVersion=10.0.17763
OutputDir={#RepoRoot}\artifacts\installers
OutputBaseFilename=CamCtrlStudio-Setup-{#AppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
SetupIconFile={#RepoRoot}\CameraControlStudio\Assets\CameraControlStudio.ico
LanguageDetectionMethod=uilanguage
ShowLanguageDialog=auto
LicenseFile={#RepoRoot}\LICENSE
CloseApplications=yes
RestartApplications=no
UninstallDisplayIcon={app}\Camera Control Studio.exe

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "danish"; MessagesFile: "compiler:Languages\Danish.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[CustomMessages]
english.NetRequired=Camera Control Studio requires .NET Framework 4.8. Install it from Microsoft, then run this installer again.
danish.NetRequired=Kamera Kontrol Studio kræver .NET Framework 4.8. Installér det fra Microsoft, og kør derefter installationsprogrammet igen.
german.NetRequired=Camera Control Studio benötigt .NET Framework 4.8. Installieren Sie es von Microsoft und starten Sie die Installation erneut.
english.UserGuides=Camera Control Studio User Guides
danish.UserGuides=Camera Control Studio Brugervejledninger
german.UserGuides=Camera Control Studio Benutzerhandbücher

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#RepoRoot}\CameraControlStudio\bin\Release\net48\Camera Control Studio.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\CameraControlStudio\bin\Release\net48\Camera Control Studio.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\CameraControlStudio\bin\Release\net48\*.dll"; Excludes: "WebView2Loader.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\CameraControlStudio\bin\Release\net48\runtimes\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#RepoRoot}\*.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RepoRoot}\docs\*"; DestDir: "{app}\docs"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#RepoRoot}\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Camera Control Studio"; Filename: "{app}\Camera Control Studio.exe"
Name: "{group}\{cm:UserGuides}"; Filename: "{win}\explorer.exe"; Parameters: """{app}\docs"""; WorkingDir: "{app}\docs"
Name: "{autodesktop}\Camera Control Studio"; Filename: "{app}\Camera Control Studio.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Camera Control Studio.exe"; Description: "{cm:LaunchProgram,Camera Control Studio}"; Flags: nowait postinstall skipifsilent

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

@echo off
cd /d "%~dp0"
dotnet build BrioPresets.csproj -c Release
if errorlevel 1 (
  echo Build failed. See the error above. The .NET SDK is required.
  pause
  exit /b 1
)
echo Application output: bin\Release\net48\Cam Pressets Studio.exe
pause

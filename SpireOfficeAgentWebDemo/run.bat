@echo off
:: SpireAgentOffice Launcher
dotnet --version >nul 2>&1 || (
    echo [ERROR] .NET SDK not found. Install from https://dotnet.microsoft.com/download
    pause & exit /b 1
)

cd /d "%~dp0"
dotnet restore SpireAgentOffice.csproj -v q
dotnet build SpireAgentOffice.csproj -v q

if exist "lib\autofixer.dat" copy /y "lib\autofixer.dat" "bin\Debug\net10.0\" >nul

set ASPNETCORE_ENVIRONMENT=Development
set ASPNETCORE_URLS=http://0.0.0.0:5184

echo Starting server...
echo Test page: http://localhost:5184/test.html
dotnet bin\Debug\net10.0\SpireAgentOffice.dll
pause

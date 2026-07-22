#!/bin/bash
# SpireAgentOffice Launcher
if ! command -v dotnet &> /dev/null; then
    echo "[ERROR] .NET SDK not found. Install from https://dotnet.microsoft.com/download ."
    exit 1
fi

cd "$(dirname "$0")"
dotnet restore --verbosity quiet SpireAgentOffice.csproj
dotnet build --verbosity quiet SpireAgentOffice.csproj

if [ -f "lib/autofixer.dat" ]; then
    cp "lib/autofixer.dat" "bin/Debug/net10.0/"
fi

export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=http://0.0.0.0:5184

echo "Starting server..."
echo "Test page: http://localhost:5184/test.html"
dotnet bin/Debug/net10.0/SpireAgentOffice.dll

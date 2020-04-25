param(
    [string]$configuration = "debug"
)

$vcVars = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\VC\Auxiliary\Build\vcvars64.bat"

&dotnet publish src/Titan.Game/Titan.Game.csproj -r win10-x64 --self-contained true -c $configuration -o bin/win10-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true
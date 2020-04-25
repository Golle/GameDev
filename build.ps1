param(
    [string]$configuration = "debug"
)

&dotnet publish Titan.sln -r win10-x64 --self-contained true -c $configuration -o bin/win10-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true
# &dotnet publish Titan.sln -r win10-x86 --self-contained true -c $configuration -o publish/win10-x86 -p:PublishSingleFile=true -p:PublishTrimmed=true
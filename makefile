Platform=AnyCPU
Configuration=Debug

all: graphtheory graphtheorytests

setuprelease:
	Platform=AnyCPU
	Configuraion=Release

setupdebug:
	Platform=AnyCPU
	Configuration=Debug

release: setuprelease all

debug: setupdebug all

nuget:
	nuget restore GraphTheory.sln

graphtheory: nuget
	xbuild GraphTheory/GraphTheory.csproj /p:Configuration=$(Configuration) /p:Platform=$(Platform)

graphtheorytests: nuget
	xbuild GraphTheory.Tests/GraphTheory.Tests.csproj /p:Configuration=$(Configuration) /p:Platform=$(Platform)

test: 
	mono ./packages/NUnit.Runners.2.6.4/tools/nunit-console.exe GraphTheory.Tests/bin/$(Configuration)/GraphTheory.Tests.dll


Platform=AnyCPU
Configuration=Debug

all: nuget graphtheory graphtheorytests

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

graphtheory:
	xbuild GraphTheory/GraphTheory.csproj /p:Configuration=$(Configuration) /p:Platform=$(Platform)

graphtheorytests:
	xbuild GraphTheory.Tests/GraphTheory.Tests.csproj /p:Configuration=$(Configuration) /p:Platform=$(Platform)

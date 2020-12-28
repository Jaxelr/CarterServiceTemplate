# Carter Service Template  [![Mit License][mit-img]][mit]

Dotnet template library for web services using the Carter library.

## Builds

| Provider | Status |
| :---: | :---: |
| Appveyor | [![Build status][appveyor-build-img]][appveyor-build] |
| Github | ![.NET][github-build] |

## Packages

| NuGet (Stable) | MyGet (Prerelease) |
| :---: | :---: |
| [![NuGet][nuget-img]][nuget] | [![MyGet][myget-img]][myget] |

### Install

For installation via the dotnet install command:

`dotnet new -i "CarterServiceTemplate::*"`

For myget installations you can specify the source on the dotnet command:

`dotnet new -i "CarterServiceTemplate::*" --nuget-source https://www.myget.org/F/carterservicetemplate/api/v3/index.json`

Then you can freely use it by executing the following dotnet command:

`dotnet new carterws -o MyCarterWs`

### Uninstall

To uninstall simply execute:

`dotnet new -u "CarterServiceTemplate"`

### Dependencies

This template targets dotnet 5.0. The following libraries are included as part of the projects:

* [Carter](https://github.com/CarterCommunity/Carter)
* [AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)
* [Carter.Cache](https://github.com/Jaxelr/Carter.Cache)
* [Insight.Database](https://github.com/jonwagner/Insight.Database)
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

[mit-img]: http://img.shields.io/badge/License-MIT-blue.svg
[mit]: https://github.com/Jaxelr/CarterServiceTemplate/blob/master/LICENSE
[appveyor-build-img]: https://ci.appveyor.com/api/projects/status/2xr17krulb7vppm7/branch/master?svg=true
[appveyor-build]: https://ci.appveyor.com/project/Jaxelr/carterservicetemplate/branch/master
[github-build]: https://github.com/Jaxelr/CarterServiceTemplate/workflows/.NET/badge.svg?branch=master
[nuget-img]: https://img.shields.io/nuget/v/CarterServiceTemplate.svg
[nuget]: https://www.nuget.org/packages/CarterServiceTemplate/
[myget-img]: https://img.shields.io/myget/carterservicetemplate/v/CarterServiceTemplate.svg
[myget]: https://www.myget.org/feed/carterservicetemplate/package/nuget/CarterServiceTemplate

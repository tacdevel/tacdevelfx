[![License](https://badgen.net/badge/license/MIT/blue)](https://github.com/tom-corwin/LibUISharp/blob/master/LICENSE.md)
[![Contributors](https://badgen.net/github/contributors/tacdevel/tcdfx)](https://github.com/tacdevel/tcdfxx/graphs/contributors)
[![CodeFactor](https://www.codefactor.io/repository/github/tacdevel/tcdfx/badge)](https://www.codefactor.io/repository/github/tacdevel/tcdfx)
[![Dependabot Status](https://api.dependabot.com/badges/status?host=github&repo=tacdevel/tcdfx)](https://dependabot.com)  
[![Open Issues](https://badgen.net/github/open-issues/tacdevel/tcdfx/)](https://github.com/tacdevel/tcdfx/issues?&q=is%3Aissue+is%3Aopen)
[![Closed Issues](https://badgen.net/github/closed-issues/tacdevel/tcdfx/)](https://github.com/tacdevel/tcdfx/issues?&q=is%3Aissue+is%3Aclosed)
[![Help Wanted](https://badgen.net/github/label-issues/tacdevel/tcdfx/help%20wanted/open)](https://github.com/tacdevel/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22)
[![Good First Issue](https://badgen.net/github/label-issues/tacdevel/tcdfx/good%20first%20issue/open)](https://github.com/tacdevel/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22)  
![C# Version](https://badgen.net/badge/C%23/7.3/green)
![Target Frameworks](https://badgen.net/badge/framework/netstandard2.0/purple)
![Supported Platforms](https://badgen.net/badge/platform/win-x64,osx-x64,linux-x64/blue?list=1)
# TCDFx (TCD Framework)


TCDFx (or the TCD Framework) is a collection of packages targeting .NET Standard, allowing users to create desktop UI applications on Windows, macOS, and Linux.

**Please Note**: This software is a work-in-progress with no stable releases yet, and is not to be considered complete nor stable, and anything is subject to change.

## Packages

| Package Name           | Development Branch | Release Branch |
| :--------------------- | :----------------- | :------------- |
| TCD.Core               | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Core-develop)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=5)<br/>![NuGet](https://badgen.net/nuget/v/TCD.Core/vpre) | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Core-master)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=6)<br/>![NuGet](https://badgen.net/nuget/v/TCD.Core) |
| TCD.Drawing.Primitives | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Drawing.Primitives-develop)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=7)<br/>![NuGet](https://badgen.net/nuget/v/TCD.Drawing.Primitives/vpre) | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Drawing.Primitives-master)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=8)<br/>![NuGet](https://badgen.net/nuget/v/TCD.Drawing.Primitives) |
| TCD.UI                 | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.UI-develop)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=9)<br/>![NuGet](https://badgen.net/nuget/v/TCD.UI/vpre) | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.UI-master)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=10)<br/>![NuGet](https://badgen.net/nuget/v/TCD.UI) |

## Contributing

Contributing is as easy as filing an issue, fixing a bug, or suggesting a new feature.

For all a list of all contributors with some stats about their contributions, see the [Contributors](https://github.com/tacdevel/tcdfxx/graphs/contributors) page.

## Building From Source

TCDFx is built with .NET Core 2.1, so you can build the packages with either Visual Studio, Visual Studio Code,
or just by running a couple simple commands. Use the steps below to get started!

### Prerequisites

| Operating System | Prerequisites                                                                                                            |
| :--------------- | :----------------------------------------------------------------------------------------------------------------------- |
| Windows 7/8.1/10 | .NET Core 2.1 SDK<br/><br/>**Optional:**<br/>Visual Studio 2017 (v15.8.x)<br/>Visual Studio Code (With the C# extension) |
| Mac OS X         | .NET Core 2.1 SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                  |
| Linux            | .NET Core 2.1 SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                  |

### Build Using Visual Studio (Windows)

*Ensure you have the latest version of Visual Studio 2017 installed with the .NET Core workload.*

1. Open the `source\TCDFx.sln` file.
2. Then, navigate to the `Build>Build Solution` menu item.

### Build Using a CLI or Visual Studio Code

Run the following command in a command-line interface in the root directory of this repository:

```
dotnet build .\source\TCDFx.sln
```

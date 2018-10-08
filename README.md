[![License](https://img.shields.io/badge/License-MIT-blue.svg?longCache=true)](https://github.com/tom-corwin/LibUISharp/blob/master/LICENSE.md)
[![CodeFactor](https://www.codefactor.io/repository/github/tacdevel/tcdfx/badge)](https://www.codefactor.io/repository/github/tacdevel/tcdfx)
![Libraries.io for GitHub](https://img.shields.io/librariesio/github/tacdevel/tcdfx.svg)  
![C# Version](https://img.shields.io/badge/C%23-7.3-05930C.svg)
![Target Frameworkss](https://img.shields.io/badge/target--frameworks-netstandard2.0-5C2D91.svg)
![Supported Platforms](https://img.shields.io/badge/supported--platforms-win--x64;osx--x64;linux--x64-blue.svg)
# TCDFx (TCD Framework)


TCDFx (or the TCD Framework) is a collection of packages targeting .NET Standard, allowing users to create desktop UI applications on Windows, macOS, and Linux.

**Please Note**: This software is a work-in-progress with no stable releases yet, and is not to be considered complete nor stable, and anything is subject to change.

## Packages

| Package Name           | Description | Development Branch | Release Branch |
| :--------------------- | :---------- | :----------------- | :------------- |
| TCD.Core               | Contains base classes, such as `Disposable`, and event delegates. | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Core-develop)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=5)<br/>![NuGet](https://img.shields.io/nuget/vpre/TCD.Core.svg) | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Core-master)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=6)<br/>![NuGet](https://img.shields.io/nuget/v/TCD./core.svg) |
| TCD.Drawing.Primitives | Contains primitive 2D drawing structures, such as `Point` and `Size`. | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Drawing.Primitives-develop)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=7)<br/>![NuGet](https://img.shields.io/nuget/vpre/TCD.Drawing.Primitives.svg) | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.Drawing.Primitives-master)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=8)<br/>![NuGet](https://img.shields.io/nuget/v/TCD.Drawing.Primitives.svg) |
| TCD.UI                 | Contains classes and types to help create UI applications using [libui](https://github.com/andlabs/libui). | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.UI-develop)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=9)<br/>![NuGet](https://img.shields.io/nuget/vpre/TCD.UI.svg) | [![Build status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/TCD.UI-master)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=10)<br/>![NuGet](https://img.shields.io/nuget/v/TCD.UI.svg) |

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
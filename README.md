# TCDFx
[![License](https://img.shields.io/badge/License-MIT-blue.svg?longCache=true)](https://github.com/tom-corwin/LibUISharp/blob/master/LICENSE.md)
<!--
[![Build status](https://ci.appveyor.com/api/projects/status/o2y9fu126dqmi4pv?svg=true)](https://ci.appveyor.com/project/tom-corwin/libuisharp)
[![CodeFactor](https://www.codefactor.io/repository/github/tom-corwin/libuisharp/badge)](https://www.codefactor.io/repository/github/tom-corwin/libuisharp)
[![NuGet](https://img.shields.io/nuget/vpre/LibUISharp.svg)](https://www.nuget.org/packages/LibUISharp)
-->
TCDFx (or the TCD Framework) is a collection of packages targeting .NET Standard, allowing users to create desktop UI applications on Windows, macOS, and Linux.

**Please Note**: This software is a work-in-progress with no releases yet, and is not to be considered complete, usable, or buildable.

## Packages

| Package Name           | Description                                                                                                         |
| :--------------------- | :------------------------------------------------------------------------------------------------------------------ |
| TCD.Core               | Contains shared code for other TCDFx packages, as well as various classes to simplify native interop.               |
| TCD.Drawing.Primitives | Contains primitive 2D drawing structures, such as Point and Size.                                                   |
| TCD.UI                 | Contains UI and Drawing/Text typess to help create UI applications using [libui](https://github.com/andlabs/libui). |


<!--
## Obtaining the Libraries

Add this line to you `csproj` project file:

```
<PackageReference Include="{PACKAGE_NAME}" Version="0.1.0" />
```
-->

## Supported Platforms

Although the TCDFx packages can be used on [any platform supported by .NET Core 2.1](https://github.com/dotnet/core/blob/master/release-notes/2.1/2.1-supported-os.md), only 64-bit platforms are currently supported.

## Contributing

Contributing is as easy as filing an issue, fixing a bug, or suggesting a new feature.

For all a list of all contributors with some stats about their contributions, see the [Contributors](https://github.com/tacdevel/TCDFx/graphs/contributors) page.

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
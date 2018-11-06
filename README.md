[![License](https://badgen.net/badge/license/MIT/blue)](https://github.com/tom-corwin/LibUISharp/blob/master/LICENSE.md)
[![Contributors](https://badgen.net/github/contributors/tacdevel/tcdfx)](https://github.com/tacdevel/tcdfxx/graphs/contributors)
[![Open Issues](https://badgen.net/github/open-issues/tacdevel/tcdfx/)](https://github.com/tacdevel/tcdfx/issues?&q=is%3Aissue+is%3Aopen)
[![Closed Issues](https://badgen.net/github/closed-issues/tacdevel/tcdfx/)](https://github.com/tacdevel/tcdfx/issues?&q=is%3Aissue+is%3Aclosed)
[![Help Wanted Issues](https://badgen.net/github/label-issues/tacdevel/tcdfx/help%20wanted/open)](https://github.com/tacdevel/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22)
[![Good First Issue](https://badgen.net/github/label-issues/tacdevel/tcdfx/good%20first%20issue/open)](https://github.com/tacdevel/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22)  
[![CodeFactor Status](https://www.codefactor.io/repository/github/tacdevel/tcdfx/badge)](https://www.codefactor.io/repository/github/tacdevel/tcdfx)
[![Dependabot Status](https://api.dependabot.com/badges/status?host=github&repo=tacdevel/tcdfx)](https://dependabot.com)  
![C# Version](https://badgen.net/badge/C%23/7.3/green)
![Target Frameworks](https://badgen.net/badge/framework/netstandard2.0/purple)

**Please Note**: This software is a work-in-progress with no stable releases yet, and is not to be
considered complete nor stable. *ANYTHING* in this repo is subject to change without notice.

# TCDFx (TCD Framework)

TCDFx (also known as the TCD Framework) is a collection of libraries targeting .NET Standard and
tools targeting .NET Core, providing APIs for UI application development, alternative P/Invoke
types, and documentation generation.

For examples, see the `examples\` directory.

## Packages

| Package Name                         | Build Status | Package Versions |
| :----------------------------------- | :----------- | :--------------- |
| TCD.Collections.MultiValueDictionary | [![Build Status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/source/TCD.Collections.MultiValueDictionary)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=10) | ![NuGet](https://badgen.net/nuget/v/TCD.Collections.MultiValueDictionary/pre) ![NuGet](https://badgen.net/nuget/v/TCD.Collections.MultiValueDictionary) |
| TCD.Disposable                       | [![Build Status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/source/TCD.Disposable)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=11) | ![NuGet](https://badgen.net/nuget/v/TCD.Disposable/pre) ![NuGet](https://badgen.net/nuget/v/TCD.Disposable) |
| TCD.Drawing.Primitives               | [![Build Status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/source/TCD.Drawing.Primitives)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=12) | ![NuGet](https://badgen.net/nuget/v/TCD.Drawing.Primitives/pre) ![NuGet](https://badgen.net/nuget/v/TCD.Drawing.Primitives) |
| TCD.InteropServices                  | [![Build Status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/source/TCD.InteropServices)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=13) | ![NuGet](https://badgen.net/nuget/v/TCD.InteropServices/pre) ![NuGet](https://badgen.net/nuget/v/TCD.InteropServices) |
| TCD.UI                               | [![Build Status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/source/TCD.UI)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=14) | ![NuGet](https://badgen.net/nuget/v/TCD.UI/pre) ![NuGet](https://badgen.net/nuget/v/TCD.UI) |
| TCDFx.Tools.DocGen                   | [![Build Status](https://dev.azure.com/tacdevel/tcdfx/_apis/build/status/tools/TCDFx.Tools.DocGen)](https://dev.azure.com/tacdevel/tcdfx/_build/latest?definitionId=15) | ![NuGet](https://badgen.net/nuget/v/TCDFx.Tools.DocGen/pre) ![NuGet](https://badgen.net/nuget/v/TCDFx.Tools.DocGen) |

### Installing TCDFx Packages

All libraries in the TCD Framework are packaged as NuGet Packages (`.nupkg`) and version numbers
use SemVer 2.0.0.

Replace `{PackageName}` with the package you want to use.  
Replace `{PackageVersion}` with the version of the package.

#### Install using Visual Studio

Open the Package Management Console, and enter the following:

```
PM> Install-Package {PackageName} -Version {PackageVersion}
```

#### Install using .NET CLI

Open a terminal (or `Command Prompt` on Windows), and enter the following:

```
dotnet add package {PackageName} --version {PackageVersion}
```

#### Install using a PackageReference

Add the following `<PackageReference />` to your `.csproj`:

```
<PackageReference Include="{PackageName}" Version="{PackageVersion}" />
```

## Contributing

Contributing is as easy as filing an issue, fixing a bug, or suggesting a new feature.

For all a list of all contributors with some stats about their contributions, see the
[Contributors](https://github.com/tacdevel/tcdfx/graphs/contributors) page.

## Building From Source

TCDFx is built with .NET Core 2.1, so you can build the packages with either Visual Studio, Visual
Studio Code, or just by running a couple simple commands. Use the steps below to get started!

### Prerequisites

| Operating System | Prerequisites |
| :--------------- | :------------ |
| Windows 7/8.1/10 | .NET Core 2.1 SDK<br/><br/>**Optional:**<br/>Visual Studio 2017 (v15.8.x)<br/>Visual Studio 2019/0x10 (v16.0.x)<br/>Visual Studio Code (With the C# extension) |
| Mac OS X         | .NET Core 2.1 SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension) |
| Linux            | .NET Core 2.1 SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension) |

### Build Using Visual Studio (Windows)

*Ensure you have the latest version of Visual Studio 2017/2019 installed with the .NET Core workload.*

1. Open the `tcdfx.sln` file.
2. Then, navigate to the `Build>Build Solution` menu item.

### Build Using a CLI or Visual Studio Code

Run the following command in a command-line interface in the root directory of this repository:

```
dotnet build .\tcdfx.sln
```
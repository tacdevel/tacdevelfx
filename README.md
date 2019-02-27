[![License][Badges.License]][Links.License]
[![Contributors][Badges.Contributors]][Links.Contributors]
![C# Version][Badges.CSharpVersion]
![Target Frameworks][Badges.TargetFrameworks]  
[![Open Issues][Badges.Issues.Open]][Links.Issues.Open]
[![Closed Issues][Badges.Issues.Closed]][Links.Issues.Closed]
[![Help Wanted Issues][Badges.Issues.HelpWanted]][Links.Issues.HelpWanted]
[![Good First Issues][Badges.Issues.GoodFirstIssue]][Links.Issues.GoodFirstIssue]  
[![CodeFactor Status][Badges.CodeFactor]][Links.CodeFactor]
[![Dependabot Status][Badges.Dependabot]][Links.Dependabot]
[![Gitter Chat][Badges.Gitter]][Links.Gitter]

# TCDFx (TCD Framework)

TCDFx (also known as the TCD Framework) is a collection of libraries targeting .NET Standard,
providing APIs for UI application development, alternative P/Invoke types, and other helpful APIs to
make development easier.

For examples, see the `examples\` directory.

## Contributing

Contributing is as easy as filing an issue, fixing a bug, or suggesting a new feature.

For more information about contributing to this project, see the
[CONTRIBUTING.md][Links.Contributing] file.

For all a list of all contributors with some stats about their contributions, see the
[Contributors][Links.Contributors] page.

## Packages

| Package Name                         | CI Build Status                                                                                            | Packages                                                          |
| :----------------------------------- | :--------------------------------------------------------------------------------------------------------- | :---------------------------------------------------------------- |
| TCD.Numerics.Hashing                 | [![Build Status][Badges.Build.3]][Links.Build.3]<br/>[![Build Stats][Badges.Build.Stats.3]][Links.Build.3]     | ![Stable][Badges.NuGet.3]<br/>![PreRelease][Badges.NuGet.Pre.3]   |
| TCD.Platform                         | [![Build Status][Badges.Build.4]][Links.Build.4]<br/>[![Build Stats][Badges.Build.Stats.4]][Links.Build.4]     | ![Stable][Badges.NuGet.4]<br/>![PreRelease][Badges.NuGet.Pre.4]   |
| TCD.Collections.MultiValueDictionary | [![Build Status][Badges.Build.5]][Links.Build.5]<br/>[![Build Stats][Badges.Build.Stats.5]][Links.Build.5]     | ![Stable][Badges.NuGet.5]<br/>![PreRelease][Badges.NuGet.Pre.5]   |
| TCD.Disposable                       | [![Build Status][Badges.Build.6]][Links.Build.6]<br/>[![Build Stats][Badges.Build.Stats.6]][Links.Build.6]     | ![Stable][Badges.NuGet.6]<br/>![PreRelease][Badges.NuGet.Pre.6]   |
| TCD.InteropServices                  | [![Build Status][Badges.Build.7]][Links.Build.7]<br/>[![Build Stats][Badges.Build.Stats.7]][Links.Build.7]     | ![Stable][Badges.NuGet.7]<br/>![PreRelease][Badges.NuGet.Pre.7]   |
| TCD.Drawing.Primitives               | [![Build Status][Badges.Build.8]][Links.Build.8]<br/>[![Build Stats][Badges.Build.Stats.8]][Links.Build.8]     | ![Stable][Badges.NuGet.8]<br/>![PreRelease][Badges.NuGet.Pre.8]   |
| TCD.Native.Libui*                    | [![Build Status][Badges.Build.9]][Links.Build.9]<br/>[![Build Stats][Badges.Build.Stats.9]][Links.Build.9]     | ![Stable][Badges.NuGet.9]<br/>![PreRelease][Badges.NuGet.Pre.9]   |
| TCD.UI                               | [![Build Status][Badges.Build.10]][Links.Build.10]<br/>[![Build Stats][Badges.Build.Stats.10]][Links.Build.10] | ![Stable][Badges.NuGet.10]<br/>![PreRelease][Badges.NuGet.Pre.10] |
| TCD.Drawing.Common                   | [![Build Status][Badges.Build.11]][Links.Build.11]<br/>[![Build Stats][Badges.Build.Stats.11]][Links.Build.11] | ![Stable][Badges.NuGet.11]<br/>![PreRelease][Badges.NuGet.Pre.11] |
| TCD.Drawing.Text                     | [![Build Status][Badges.Build.12]][Links.Build.12]<br/>[![Build Stats][Badges.Build.Stats.12]][Links.Build.12] | ![Stable][Badges.NuGet.12]<br/>![PreRelease][Badges.NuGet.Pre.12] |

<sub>* - This package does not contain public members and should not be added directly.</sub>

### Installing TCDFx Packages

All libraries in the TCD Framework are packaged as NuGet Packages (`.nupkg`) and version numbers use
SemVer 2.0.0.

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

## Building From Source

TCDFx is built with .NET Core 2.1, so you can build the packages with either Visual Studio, Visual
Studio Code, or just by running a couple simple commands. Use the steps below to get started!

### Prerequisites

| Operating System | Prerequisites                                                                                                                                             |
| :--------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Windows 7/8.1/10 | .NET Core 2.2 SDK<br/><br/>**Optional:**<br/>Visual Studio 2017 (v15.9.x)<br/>Visual Studio 2019 (v16.0.x)<br/>Visual Studio Code (With the C# extension) |
| Mac OS X         | .NET Core 2.2 SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                                                   |
| Linux            | .NET Core 2.2 SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                                                   |

### Build Using Visual Studio (Windows)

*Ensure you have the latest version of Visual Studio 2017/2019 installed with the .NET Core
workload.*

1. Open the `tcdfx.sln` file.
2. Then, navigate to the `Build>Build Solution` menu item.

### Build Using a CLI or Visual Studio Code

Run the following command in a command-line interface in the root directory of this repository:

```
dotnet build .\tcdfx.sln
```

<!-- Images/Badges -->
[Badges.Build.3]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Numerics.Hashing
[Badges.Build.4]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Platform
[Badges.Build.5]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Collections.MultiValueDictionary
[Badges.Build.6]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Disposable
[Badges.Build.7]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.InteropServices
[Badges.Build.8]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Drawing.Primitives
[Badges.Build.9]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Native.Libui
[Badges.Build.10]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.UI
[Badges.Build.11]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Drawing.Common
[Badges.Build.12]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/source/TCD.Drawing.Text
[Badges.Build.Stats.3]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/3?showStats=false
[Badges.Build.Stats.4]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/4?showStats=false
[Badges.Build.Stats.5]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/5?showStats=false
[Badges.Build.Stats.6]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/6?showStats=false
[Badges.Build.Stats.7]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/7?showStats=false
[Badges.Build.Stats.8]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/8?showStats=false
[Badges.Build.Stats.9]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/9?showStats=false
[Badges.Build.Stats.10]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/10?showStats=false
[Badges.Build.Stats.11]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/11?showStats=false
[Badges.Build.Stats.12]: https://buildstats.info/azurepipelines/chart/tom-corwin/tcdfx/12?showStats=false
[Badges.NuGet.3]: https://buildstats.info/nuget/TCD.Numerics.Hashing
[Badges.NuGet.4]: https://buildstats.info/nuget/TCD.Platform
[Badges.NuGet.5]: https://buildstats.info/nuget/TCD.Collections.MultiValueDictionary
[Badges.NuGet.6]: https://buildstats.info/nuget/TCD.Disposable
[Badges.NuGet.7]: https://buildstats.info/nuget/TCD.InteropServices
[Badges.NuGet.8]: https://buildstats.info/nuget/TCD.Drawing.Primitives
[Badges.NuGet.9]: https://buildstats.info/nuget/TCD.Native.Libui
[Badges.NuGet.10]: https://buildstats.info/nuget/TCD.UI
[Badges.NuGet.11]: https://buildstats.info/nuget/TCD.Drawing.Common
[Badges.NuGet.12]: https://buildstats.info/nuget/TCD.Drawing.Text
[Badges.NuGet.Pre.3]: https://buildstats.info/nuget/TCD.Numerics.Hashing?includePreReleases=true
[Badges.NuGet.Pre.4]: https://buildstats.info/nuget/TCD.Platform?includePreReleases=true
[Badges.NuGet.Pre.5]: https://buildstats.info/nuget/TCD.Collections.MultiValueDictionary?includePreReleases=true
[Badges.NuGet.Pre.6]: https://buildstats.info/nuget/TCD.Disposable?includePreReleases=true
[Badges.NuGet.Pre.7]: https://buildstats.info/nuget/TCD.InteropServices?includePreReleases=true
[Badges.NuGet.Pre.8]: https://buildstats.info/nuget/TCD.Drawing.Primitives?includePreReleases=true
[Badges.NuGet.Pre.9]: https://buildstats.info/nuget/TCD.Native.Libui?includePreReleases=true
[Badges.NuGet.Pre.10]: https://buildstats.info/nuget/TCD.UI?includePreReleases=true
[Badges.NuGet.Pre.11]: https://buildstats.info/nuget/TCD.Drawing.Common?includePreReleases=true
[Badges.NuGet.Pre.12]: https://buildstats.info/nuget/TCD.Drawing.Text?includePreReleases=true
[Badges.License]: https://badgen.net/badge/license/MIT/blue
[Badges.Contributors]: https://badgen.net/github/contributors/tom-corwin/tcdfx
[Badges.CSharpVersion]: https://badgen.net/badge/C%23/8.0/green
[Badges.TargetFrameworks]: https://badgen.net/badge/targets/netstandard2.0/purple
[Badges.Issues.Open]: https://badgen.net/github/open-issues/tom-corwin/tcdfx/
[Badges.Issues.Closed]: https://badgen.net/github/closed-issues/tom-corwin/tcdfx/
[Badges.Issues.HelpWanted]: https://badgen.net/github/label-issues/tom-corwin/tcdfx/help%20wanted/open
[Badges.Issues.GoodFirstIssue]: https://badgen.net/github/label-issues/tom-corwin/tcdfx/good%20first%20issue/open
[Badges.CodeFactor]: https://www.codefactor.io/repository/github/tom-corwin/tcdfx/badge
[Badges.Dependabot]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Badges.Gitter]: https://badgen.net/badge/chat/on%20gitter/cyan

<!-- Links -->
[Links.Build.3]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=3
[Links.Build.4]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=4
[Links.Build.5]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=5
[Links.Build.6]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=6
[Links.Build.7]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=7
[Links.Build.8]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=8
[Links.Build.9]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=9
[Links.Build.10]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=10
[Links.Build.11]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=11
[Links.Build.12]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=12
[Links.License]: https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
[Links.Contributors]: https://github.com/tom-corwin/tcdfx/graphs/contributors
[Links.Issues.Open]: https://github.com/tom-corwin/tcdfx/issues?&q=is%3Aissue+is%3Aopen
[Links.Issues.Closed]: https://github.com/tom-corwin/tcdfx/issues?&q=is%3Aissue+is%3Aclosed
[Links.Issues.HelpWanted]: https://github.com/tom-corwin/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22
[Links.Issues.GoodFirstIssue]: https://github.com/tom-corwin/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22
[Links.CodeFactor]: https://www.codefactor.io/repository/github/tom-corwin/tcdfx
[Links.Dependabot]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Links.Gitter]: https://gitter.im/tom-corwin/tcdfx?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge
[Links.Contributing]: https://github.com/tom-corwin/tcdfx/blob/master/CONTRIBUTING.md

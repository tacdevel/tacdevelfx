# TCDFx

[![Codacy Status][Badges.Codacy]][Links.Codacy]
[![Dependabot Status][Badges.Dependabot]][Links.Dependabot]
[![Gitter Chat][Badges.Gitter]][Links.Gitter]

TCDFx is a small collection of libraries targeting .NET Standard 2.1 Preview.

<!--TODO: Add a sentence or two to the summary.-->

**Notice**: *This project is currently a work-in-progress, and should not be used in a production environment. Anything is subject to change at anytime.*

## Builds and Releases

**Notice**: *There are currently no packages released to NuGet yet, so some badges may be broken.*  
**Notice**: *The first pre-release packages will be released after [andlabs/libui:remodel](https://github.com/andlabs/libui/tree/remodel) is merged into [andlabs/libui:master](https://github.com/andlabs/libui/tree/master) since there are major changes to the native `libui` API.*

|                      | TCDFx.Core <sub><sub>![Stable][TCDFxCore.Packages.Badge]</sub></sub>                                                                                                                                                                                            | TCDFx.Core <sub><sub>![Stable][TCDFxCore.Packages.Badge]</sub></sub>                                                                                                                                                                                    |
| :------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Windows 8.1**      | Debug: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Windows81Debug.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Windows81Release.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub>           | Debug: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Windows81Debug.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Windows81Release.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub>           |
| **Windows 10 v1803** | Debug: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Windows10v1803Debug.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Windows10v1803Release.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub> | Debug: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Windows10v1803Debug.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Windows10v1803Release.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub> |
| **Windows 10 v1809** | Debug: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Windows10v1809Debug.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Windows10v1809Release.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub> | Debug: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Windows10v1809Debug.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Windows10v1809Release.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub> |
| **Ubuntu 16.04**     | Debug: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Ubuntu1604Debug.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.Ubuntu1604Release.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub>         | Debug: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Ubuntu1604Debug.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.Ubuntu1604Release.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub>         |
| **macOS 10.13**      | Debug: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.macOS1013Debug.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.macOS1013Release.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub>           | Debug: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.macOS1013Debug.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.macOS1013Release.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub>           |
| **macOS 10.14**      | Debug: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.macOS1014Debug.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxCore.BuildStatus.macOS1014Release.Badge]][TCDFxCore.BuildStatus.Link]</sub></sub>           | Debug: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.macOS1014Debug.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub><br/>Release: <sub><sub>[![Build Status][TCDFxUI.BuildStatus.macOS1014Release.Badge]][TCDFxUI.BuildStatus.Link]</sub></sub>           |

## Using TCDFx Libraries

For examples, see the `examples\` directory.

Until the first stable packages are released, you can either build the libraries from source, or use unstable pre-built packages from our [CI build feed](https://www.myget.org/feed/Details/tcdfx-build).

```
 https://www.myget.org/F/tcdfx-build/api/v3/index.json
```

### Using Pre-Built Packages

While following these instructions:

  * Replace `{PackageName}` with the package you want to use.  
  * Replace `{PackageVersion}` with the version of the package.

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

Add the following to your `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="{PackageName}" Version="{PackageVersion}" />
</ItemGroup>
```

### Building From Source

You can build the packages with either Visual Studio 2019.1+, Visual Studio Code, or just by running a few simple commands. Use the steps below to get started!

| Operating System | Prerequisites                                                                                                                                             |
| :--------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Windows 7/8.1/10 | .NET Core 3.0 Preview 6+ SDK<br/><br/>**Optional:**<br/>Visual Studio 2019.1+<br/>Visual Studio Code (With the C# extension)                              |
| Mac OS X         | .NET Core 3.0 Preview 6+ SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                                        |
| Linux            | .NET Core 3.0 Preview 6+ SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                                        |

#### Build Using Visual Studio (Windows)

*Ensure you have the latest version of Visual Studio 2019 installed with the .NET Core
workload, installed .NET Core 3.0 Preview 6, and enabled the use of preview versions of
.NET Core in Visual Studio's options.*

  1. Open the `.\TCDFx.sln` file.
  2. Then, navigate to the `Build>Build Solution` menu item.

#### Build Using a CLI or Visual Studio Code

Run the following command in a command-line interface in the root directory of this repository:

```
dotnet build .\TCDFx.sln
```

## Contributing

[![Contributors][Badges.Contributors]][Links.Contributors]
[![Open Issues][Badges.Issues.Open]][Links.Issues.Open]
[![Closed Issues][Badges.Issues.Closed]][Links.Issues.Closed]
[![Help Wanted Issues][Badges.Issues.HelpWanted]][Links.Issues.HelpWanted]
[![Good First Issues][Badges.Issues.GoodFirstIssue]][Links.Issues.GoodFirstIssue]

Contributing is as easy as filing an issue, fixing a bug, or suggesting a new feature.

For more information about contributing to this project, see the [CONTRIBUTING.md][Links.Contributing] file.

For all a list of all contributors with some stats about their contributions, see the [Contributors][Links.Contributors] page.

[Badges.Codacy]: https://api.codacy.com/project/badge/Grade/2140aa3a23a848a28391aa3c778b9526
[Badges.Dependabot]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Badges.Gitter]: https://badgen.net/badge/chat/on%20gitter/cyan
[Links.Codacy]: https://www.codacy.com/app/tom-corwin/tcdfx?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=tom-corwin/tcdfx&amp;utm_campaign=Badge_Grade
[Links.Dependabot]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Links.Gitter]: https://gitter.im/tom-corwin/tcdfx?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge
[TCDFxCore.BuildStatus.Windows81Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows81_Debug
[TCDFxCore.BuildStatus.Windows81Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows81_Debug
[TCDFxCore.BuildStatus.Windows10v1803Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows10v1803_Debug
[TCDFxCore.BuildStatus.Windows10v1803Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows10v1803_Release
[TCDFxCore.BuildStatus.Windows10v1809Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows10v1809_Debug
[TCDFxCore.BuildStatus.Windows10v1809Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows10v1809_Debug
[TCDFxCore.BuildStatus.Ubuntu1604Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Ubuntu1604_Debug
[TCDFxCore.BuildStatus.Ubuntu1604Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Ubuntu1604_Release
[TCDFxCore.BuildStatus.macOS1013Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1013_Debug
[TCDFxCore.BuildStatus.macOS1013Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1013_Release
[TCDFxCore.BuildStatus.macOS1014Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1014_Debug
[TCDFxCore.BuildStatus.macOS1014Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1014_Release
[TCDFxUI.BuildStatus.Windows81Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows81_Debug
[TCDFxUI.BuildStatus.Windows81Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows81_Debug
[TCDFxUI.BuildStatus.Windows10v1803Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows10v1803_Debug
[TCDFxUI.BuildStatus.Windows10v1803Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows10v1803_Release
[TCDFxUI.BuildStatus.Windows10v1809Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows10v1809_Debug
[TCDFxUI.BuildStatus.Windows10v1809Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows10v1809_Debug
[TCDFxUI.BuildStatus.Ubuntu1604Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Ubuntu1604_Debug
[TCDFxUI.BuildStatus.Ubuntu1604Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Ubuntu1604_Release
[TCDFxUI.BuildStatus.macOS1013Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1013_Debug
[TCDFxUI.BuildStatus.macOS1013Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1013_Release
[TCDFxUI.BuildStatus.macOS1014Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1014_Debug
[TCDFxUI.BuildStatus.macOS1014Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1014_Release
[TCDFxCore.BuildStatus.Link]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=15&branchName=master
[TCDFxUI.BuildStatus.Link]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=16&branchName=master
[TCDFxCore.Packages.Badge]: https://badgen.net/nuget/v/TCDFx.Core?color=blue&label=stable
[TCDFxUI.Packages.Badge]: https://badgen.net/nuget/v/TCDFx.UI?color=blue&label=stable
[Badges.Contributors]: https://badgen.net/github/contributors/tom-corwin/tcdfx
[Badges.Issues.Open]: https://badgen.net/github/open-issues/tom-corwin/tcdfx/
[Badges.Issues.Closed]: https://badgen.net/github/closed-issues/tom-corwin/tcdfx/
[Badges.Issues.HelpWanted]: https://badgen.net/github/label-issues/tom-corwin/tcdfx/help%20wanted/open
[Badges.Issues.GoodFirstIssue]: https://badgen.net/github/label-issues/tom-corwin/tcdfx/good%20first%20issue/open
[Links.Contributors]: https://github.com/tom-corwin/tcdfx/graphs/contributors
[Links.Issues.Open]: https://github.com/tom-corwin/tcdfx/issues?&q=is%3Aissue+is%3Aopen
[Links.Issues.Closed]: https://github.com/tom-corwin/tcdfx/issues?&q=is%3Aissue+is%3Aclosed
[Links.Issues.HelpWanted]: https://github.com/tom-corwin/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22
[Links.Issues.GoodFirstIssue]: https://github.com/tom-corwin/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22
[Links.Contributing]: https://github.com/tom-corwin/tcdfx/blob/master/CONTRIBUTING.md
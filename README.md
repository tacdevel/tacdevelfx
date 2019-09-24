# TCDFx<br/>![Repository Size][GitHub.RepoSize.Badge] ![Repository Top Language][GitHub.RepoLang.Badge] [![Contributors][GitHub.Contributors.Badge]][GitHub.Contributors.Link] [![Codacy Status][Codacy.Badge]][Codacy.Link] [![Dependabot Status][Dependabot.Badge]][Dependabot.Link] [![Gitter Chat][Gitter.Badge]][Gitter.Link]

[GitHub.RepoSize.Badge]: https://img.shields.io/github/repo-size/tom-corwin/tcdfx.svg?color=grey&label=Size&logo=github
[GitHub.RepoLang.Badge]: https://img.shields.io/github/languages/top/tom-corwin/tcdfx.svg?color=grey&label=C%23&logo=github
[GitHub.Contributors.Badge]: https://img.shields.io/github/contributors/tom-corwin/tcdfx.svg?color=grey&label=Contributors&logo=github
[GitHub.Contributors.Link]: https://github.com/tom-corwin/tcdfx/graphs/contributors
[Codacy.Badge]: https://img.shields.io/codacy/grade/2140aa3a23a848a28391aa3c778b9526/master.svg?label=Codacy+Grade&logo=codacy
[Codacy.Link]: https://www.codacy.com/app/tom-corwin/tcdfx?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=tom-corwin/tcdfx&amp;utm_campaign=Badge_Grade
[Dependabot.Badge]: https://badgen.net/dependabot/tom-corwin/tcdfx?icon=dependabot
[Dependabot.Link]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Gitter.Badge]: https://img.shields.io/gitter/room/tom-corwin/tcdfx.svg?label=Chat&logo=gitter
[Gitter.Link]: https://gitter.im/tom-corwin/tcdfx?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge

<!--TODO: Add 1-2 paragraphs summarizing TCDFx. -->

<small>**This README is being rewritten, and may contain outdated or missing information.**</small>

TCDFx is a small collection of cross-platform packages targeting .NET Standard 2.1 that provide helper classes, alternate P/Invoke implementations, and an API for GUI development (using [andlabs/libui](https://github.com/andlabs/libui)).

## Project Status

Currently, this project should still be considered a work-in-progress and should not be used in a production environment. Anything is subject to change at anytime (e.g.: folder restructuring, breaking API changes, etc.).

## Contributing

[![Open Issues][GitHub.Issues.Open.Badge]][GitHub.Issues.Open.Link] [![Closed Issues][GitHub.Issues.Closed.Badge]][GitHub.Issues.Closed.Link] [![Help Wanted Issues][GitHub.Issues.HelpWanted.Badge]][GitHub.Issues.HelpWanted.Link] [![Good First Issues][GitHub.Issues.GoodFirstIssue.Badge]][GitHub.Issues.GoodFirstIssue.Link]

Contributing is as easy as filing an issue, fixing a bug, or suggesting a new feature. For more information about contributing to this project, see the [CONTRIBUTING.md][File.Contributing.Link] file.

[GitHub.Issues.Open.Badge]: https://img.shields.io/github/issues-raw/tom-corwin/tcdfx.svg?color=grey&label=Open%20Issues&logo=github
[GitHub.Issues.Open.Link]: https://github.com/tom-corwin/tcdfx/issues?&q=is%3Aissue+is%3Aopen
[GitHub.Issues.Closed.Badge]: https://img.shields.io/github/issues-closed-raw/tom-corwin/tcdfx.svg?color=grey&label=Closed%20Issues&logo=github
[GitHub.Issues.Closed.Link]: https://github.com/tom-corwin/tcdfx/issues?&q=is%3Aissue+is%3Aclosed
[GitHub.Issues.HelpWanted.Badge]: https://img.shields.io/github/issues-raw/tom-corwin/tcdfx/HelpWanted.svg?color=grey&label=Help%20Wanted%20Issues&logo=github
[GitHub.Issues.HelpWanted.Link]: https://github.com/tom-corwin/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22HelpWanted%22
[GitHub.Issues.GoodFirstIssue.Badge]: https://img.shields.io/github/issues-raw/tom-corwin/tcdfx/GoodFirstIssue.svg?color=grey&label=Good%20First%20Issues&logo=github
[GitHub.Issues.GoodFirstIssue.Link]: https://github.com/tom-corwin/tcdfx/issues?q=is%3Aissue+is%3Aopen+label%3A%22GoodFirstIssue%22
[File.Contributing.Link]: https://github.com/tom-corwin/tcdfx/blob/master/CONTRIBUTING.md

### Build Status

We use [Azure Pipelines][AzurePipelines.Link] for our CI builds. Although we currently only run builds on the following platforms, TCDFx can be built and used on any [platform supported by .NET Core 3.0][DotNetCore.SupportedOS.Link].

| <big><sub>Platform</sub>&nbsp;<sup>Package</sup></big> | TCDFx.Core | TCDFx.UI  |
| ------------------------------------------------------ | ---------: | --------: |
| **Windows 8.1<br/><br/>**  | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_Windows81_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_Windows81_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small>   | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_Windows81_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_Windows81_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small> |
| **Windows 10<br/><br/>**   | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_Windows10_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_Windows10_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small>   | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_Windows10_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_Windows10_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small> |
| **Ubuntu 16.04<br/><br/>** | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_Ubuntu1604_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_Ubuntu1604_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small> | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_Ubuntu1604_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_Ubuntu1604_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small> |
| **macOS 10.13<br/><br/>**  | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_macOS1013_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_macOS1013_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small>   | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_macos1013_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_macos1013_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small> |
| **macOS 10.14<br/><br/>**  | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_macOS1014_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxCore_macOS1014_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small>   | <small>Debug: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_macos1014_Debug.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub><br/>Release: <sub><sub><sub><sub><sub><sub><sub>[![Build Status][BuildStatus.TCDFxUI_macos1014_Release.Badge]][BuildStatus.Link]</sub></sub></sub></sub></sub></sub></sub></small> |

[AzurePipelines.Link]: https://azure.microsoft.com/en-us/services/devops/pipelines/
[DotNetCore.SupportedOS.Link]: https://github.com/dotnet/core/blob/master/release-notes/3.0/3.0-supported-os.md
[BuildStatus.Link]: https://dev.azure.com/tom-corwin/tcdfx/_build/latest?definitionId=15&branchName=master
[BuildStatus.TCDFxCore_Windows81_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows81_Debug
[BuildStatus.TCDFxCore_Windows81_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows81_Release
[BuildStatus.TCDFxCore_Windows10_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows10_Debug
[BuildStatus.TCDFxCore_Windows10_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Windows10_Release
[BuildStatus.TCDFxCore_Ubuntu1604_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Ubuntu1604_Debug
[BuildStatus.TCDFxCore_Ubuntu1604_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_Ubuntu1604_Release
[BuildStatus.TCDFxCore_macOS1013_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1013_Debug
[BuildStatus.TCDFxCore_macOS1013_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1013_Release
[BuildStatus.TCDFxCore_macOS1014_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1014_Debug
[BuildStatus.TCDFxCore_macOS1014_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxCore_macOS1014_Release
[BuildStatus.TCDFxUI_Windows81_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows81_Debug
[BuildStatus.TCDFxUI_Windows81_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows81_Release
[BuildStatus.TCDFxUI_Windows10_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows10_Debug
[BuildStatus.TCDFxUI_Windows10_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Windows10_Release
[BuildStatus.TCDFxUI_Ubuntu1604_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Ubuntu1604_Debug
[BuildStatus.TCDFxUI_Ubuntu1604_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_Ubuntu1604_Release
[BuildStatus.TCDFxUI_macOS1013_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1013_Debug
[BuildStatus.TCDFxUI_macOS1013_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1013_Release
[BuildStatus.TCDFxUI_macOS1014_Debug.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1014_Debug
[BuildStatus.TCDFxUI_macOS1014_Release.Badge]: https://dev.azure.com/tom-corwin/tcdfx/_apis/build/status/TCDFx?branchName=master&jobName=TCDFxUI_macOS1014_Release

### Current Packages

`TCDFx.UI`, although being built by the CI, will not have any packages released to NuGet (yet) because the API in [andlabs/libui](https://github.com/andlabs/libui) is currently being rewritten in [this branch](https://github.com/andlabs/libui/tree/remodel).

<!--TODO: Add description of versioning scheme. -->

| Package    | Versions  |
| :--------- | --------: |
| TCDFx.Core | ![Stable][Versions.TCDFxCore.Stable.Badge]<br/>![Preview][Versions.TCDFxCore.Preview.Badge] |
| TCDFx.UI   | ![Stable][Versions.TCDFxUI.Stable.Badge]<br/>![Preview][Versions.TCDFxUI.Preview.Badge] |

Stable and preview packages will be released onto NuGet (when they are released).

CI builds are are published to our [GitHub Package Registry](https://github.com/tom-corwin/tcdfx/packages).

[Versions.TCDFxCore.Stable.Badge]: https://img.shields.io/nuget/v/TCDFx.Core.svg?color=blue&label=Stable&logo=nuget
[Versions.TCDFxCore.Preview.Badge]: https://img.shields.io/nuget/vpre/TCDFx.Core.svg?color=orange&label=Preview&logo=nuget
[Versions.TCDFxUI.Stable.Badge]: https://img.shields.io/nuget/v/TCDFx.UI.svg?color=blue&label=Stable&logo=nuget
[Versions.TCDFxUI.Preview.Badge]: https://img.shields.io/nuget/vpre/TCDFx.UI.svg?color=orange&label=Preview&logo=nuget

<!--TODO: Package Links -->

## Using TCDFx Packages

For examples, see the `examples\` directory.

### Runtime Prerequisites

| Operating System | Prerequisites                                  |
| :--------------- | :----------------------------------------------|
| Windows 7/8.1/10 | Microsoft .NET Core 3.0 Runtime                |
| Linux            | Microsoft .NET Core 3.0 Runtime<br/>GTK+ 3.10+ |
| macOS            | Microsoft .NET Core 3.0 Runtime                |

### Utilizing Pre-Built Packages

While following these instructions:

  * Replace `{PackageName}` with the package you want to use.  
  * Replace `{PackageVersion}` with the version of the package.

#### Install using .NET CLI

Run the following command in a command-line interface and enter the following:

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

You can build the packages just by installing the prerequisites and running a few commands. Use the steps below to get started!

#### Build Prerequisites

| Operating System | Prerequisites               |
| :--------------- | :---------------------------|
| Windows 7/8.1/10 | Microsoft .NET Core 3.0 SDK |
| Linux            | Microsoft .NET Core 3.0 SDK |
| macOS            | Microsoft .NET Core 3.0 SDK |

#### Build Using a CLI

Run the following command in a command-line interface in the root directory of this repository (with respect to OS):

```
dotnet build dirs.proj
```

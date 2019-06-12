# TCDFx

[![Codacy Status][Badges.Codacy]][Links.Codacy]
[![Dependabot Status][Badges.Dependabot]][Links.Dependabot]
[![Gitter Chat][Badges.Gitter]][Links.Gitter]

TCDFx is a small collection of libraries targeting .NET Standard 2.1 Preview.

<!--TODO: Add a sentence or two to the summary.-->

**Notice**: *This project is currently a work-in-progress, and should not be used in a production environment. Anything is subject to change at anytime.*

[Badges.Codacy]: https://api.codacy.com/project/badge/Grade/2140aa3a23a848a28391aa3c778b9526
[Badges.Dependabot]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Badges.Gitter]: https://badgen.net/badge/chat/on%20gitter/cyan
[Links.Codacy]: https://www.codacy.com/app/tom-corwin/tcdfx?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=tom-corwin/tcdfx&amp;utm_campaign=Badge_Grade
[Links.Dependabot]: https://api.dependabot.com/badges/status?host=github&repo=tom-corwin/tcdfx
[Links.Gitter]: https://gitter.im/tom-corwin/tcdfx?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge

### Builds and Releases

**Notice**: *The first pre-release packages will be released after [andlabs/libui:remodel](https://github.com/andlabs/libui/tree/remodel) is merged into [andlabs/libui:master](https://github.com/andlabs/libui/tree/master) since there are going to be major changes to the native `libui` API.*

| Package Name | Build Status                                                               | Latest Releases                                                                      |
| :----------- | :--------------------------------------------------------------------------| :----------------------------------------------------------------------------------: |
| TCDFx.Core   | [![Build Status][TCDFxCore.BuildStatus.Badge]][TCDFxCore.BuildStatus.Link] | ![Stable][TCDFxCore.Packages.Badge]<br/>![Pre-Release][TCDFxCore.Packages.Badge.Pre] |
| TCDFx.UI     | [![Build Status][TCDFxUI.BuildStatus.Badge]][TCDFxUI.BuildStatus.Link]     | ![Stable][TCDFxUI.Packages.Badge]<br/>![Pre-Release][TCDFxUI.Packages.Badge.Pre]     |


[TCDFxCore.BuildStatus.Badge]: https://dev.azure.com/tom-corwin/tcdfx-build/_apis/build/status/TCDFx.Core?branchName=master
[TCDFxCore.BuildStatus.Link]: https://dev.azure.com/tom-corwin/tcdfx-build/_build/latest?definitionId=15
[TCDFxUI.BuildStatus.Badge]: https://dev.azure.com/tom-corwin/tcdfx-build/_apis/build/status/TCDFx.UI?branchName=master
[TCDFxUI.BuildStatus.Link]: https://dev.azure.com/tom-corwin/tcdfx-build/_build/latest?definitionId=16
[TCDFxCore.Packages.Badge]: https://badgen.net/nuget/v/TCDFx.Core?color=blue&label=stable
[TCDFxCore.Packages.Badge.Pre]: https://badgen.net/nuget/v/TCDFx.Core/pre?color=cyan&label=pre-release
[TCDFxUI.Packages.Badge]: https://badgen.net/nuget/v/TCDFx.UI?color=blue&label=stable
[TCDFxUI.Packages.Badge.Pre]: https://badgen.net/nuget/v/TCDFx.UI/pre?color=cyan&label=pre-release

## Using TCDFx Libraries

For examples, see the `examples\` directory.

Until the first packages are released, you must build the libraries from source.
<!--
You can either use pre-built libraries hosted on NuGet, or build the libraries from source.

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
-->

### Building From Source

You can build the packages with either Visual Studio 2019.1+, Visual Studio Code, or just by running a few simple commands. Use the steps below to get started!

| Operating System | Prerequisites                                                                                                                                             |
| :--------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Windows 7/8.1/10 | .NET Core 3.0 Preview 5+ SDK<br/><br/>**Optional:**<br/>Visual Studio 2019.1+<br/>Visual Studio Code (With the C# extension) |
| Mac OS X         | .NET Core 3.0 Preview 5+ SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                                                   |
| Linux            | .NET Core 3.0 Preview 5+ SDK<br/><br/>**Optional:**<br/>Visual Studio Code (With the C# extension)                                                                   |

#### Build Using Visual Studio (Windows)

*Ensure you have the latest version of Visual Studio 2019 installed with the .NET Core
workload, installed .NET Core 3.0 Preview 5, and enabled the use of preview versions of
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
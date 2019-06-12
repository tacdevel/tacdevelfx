# Contributing to TCDFx

It's great to see the community taking a part in the development and evolution of TCDFx. This document's goal is to make contributing to this project as simple and transparent as possible, whether it's:

  - Reporting a bug or other issue.
  - Discussing the current state of the code.
  - Submitting a bug fix.
  - Proposing a new feature.
  - Becoming a maintainer.

We use [GitHub](https://github.com)'s services to host our code, track issues and releases, and accept code changes via pull requests. We also use the following [GitHub Probots](https://probot.github.io/) for the described purpose(s):

  - [triage-new-issues](https://github.com/tunnckoCore/triage-new-issues): Adds a `triage` label to new issues/PRs that have no labels.
  - [request-info](https://probot.github.io/apps/request-info): Requests more information from new issues and PRs with generic titles and/or empty descriptions.
  - [pr-triage](https://github.com/pr-triage/pr-triage): Adds labels to a PR depending on the review status.
  - [Mergeable](https://github.com/mergeability/mergeable): Adds a check for a `NO-MERGE` label.
  - [helPR](https://github.com/rsarky/helpr): Adds labels to issues with information about a related PR.

## Pull Requests

In the next couple months, this project will begin using [GitHub Flow](https://guides.github.com/introduction/flow/index.html). This means that all code changes will happen via pull requests.

Submitting a pull request is easy. Just follow the steps below:

  1. Fork the repository and create a new branch off of `master`
  2. If there is testable code, add tests (if you know how to write them)
  3. If you have changed/added any public APIs, update the corresponding documentation.
  4. Ensure to run all tests (if any) and make sure they pass.
  5. Make sure your documentation follows our [coding styles](#coding-styles)
  6. [Submit your pull request](https://github.com/tom-corwin/tcdfx/pull/new)!

### Coding Styles

Basic guidelines are listed below:

  - Spaces, not tabs
  - 4 spaces per tab
  - No trailing whitespace (including a new line at the end of a file)

For the most part, everything should be formatted as per the suggestions in the error list in either Visual Studio or Visual Studio Code, since we use an `.editorconfig` file.

## Issue Reporting

Reporting an issue is even easier than submitting a pull request. Just follow the step below to write and report an issue/bug:

  1. [Write a descriptive issue and submit](https://github.com/tom-corwin/tcdfx/issue/new)
    - Provide a summary of the issue you would like to report.
    - Explain what should have happened.
    - Explain what actually happens.
    - Show the steps you need to take to reproduce the problem. be as specific as you can, and provide sample code if possible.
    - Add any additional notes, including operating system information.

## Contribution Licensing

When you submit a pull request or issue, your submissions are understood to be under the same [license](https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md) that covers the project. Feel free to contact a project maintainer if that is a concern.
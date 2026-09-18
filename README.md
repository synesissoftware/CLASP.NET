# CLASP.NET <!-- omit in toc -->

**C**ommand-**L**ine **A**rgument **S**orting and **P**arsing, for .NET

![Language](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white)
[![License](https://img.shields.io/badge/License-BSD_3--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)
[![GitHub release](https://img.shields.io/github/v/release/synesissoftware/CLASP.NET.svg)](https://github.com/synesissoftware/CLASP.NET/releases/latest)
[![Last Commit](https://img.shields.io/github/last-commit/synesissoftware/CLASP.NET)](https://github.com/synesissoftware/CLASP.NET/commits/master)
[![CI](https://github.com/synesissoftware/CLASP.NET/actions/workflows/ci.yml/badge.svg)](https://github.com/synesissoftware/CLASP.NET/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/CLASP.NET.svg)](https://www.nuget.org/packages/CLASP.NET/)
![TFM](https://img.shields.io/badge/TFM-net8.0%20%7C%20netstandard2.0-lightgrey)


## Table of Contents <!-- omit in toc -->

- [Introduction](#introduction)
- [Installation](#installation)
- [Quick start](#quick-start)
- [Platform support](#platform-support)
- [Repository layout](#repository-layout)
- [Building from source](#building-from-source)
- [Examples](#examples)
- [Project Information](#project-information)
  - [Where to get help](#where-to-get-help)
  - [Contribution guidelines](#contribution-guidelines)
  - [Dependencies](#dependencies)
  - [Related projects](#related-projects)
  - [License](#license)


## Introduction

**CLASP** stands for Command-Line Argument Sorting and Parsing. The first
CLASP library was a C library with a C++ wrapper. There are implementations
in several languages. **CLASP.NET** is the **.NET** version.

From **0.27.0**, the repository ships as an SDK-style multi-target library
(`net8.0`, `netstandard2.0`) with CI and NuGet packaging. The public
namespaces remain **`Clasp`** / **`Clasp.Util`** / **`Clasp.Binding`** (and
related) for continuity with Framework-era consumers such as
**libCLImate.NET**.


## Installation

```bash
dotnet add package CLASP.NET
```

See [INSTALL.md](./INSTALL.md) for source checkout restore, build, and test.


## Quick start

```csharp
using Clasp;
using Clasp.Util;

Console.WriteLine($"CLASP.NET {LibraryVersion.VersionString}");

var arguments = new Arguments(
    args,
    new Specification[]
    {
        UsageUtil.Help,
        UsageUtil.Version,
    });
```

See [`samples/CLASP.NET.QuickStart`](./samples/CLASP.NET.QuickStart) for a
runnable example. A short index is in [EXAMPLES.md](./EXAMPLES.md).


## Platform support

The library multi-targets:

| Target | Rationale |
| --- | --- |
| `net8.0` | Modern .NET runtime |
| `netstandard2.0` | Broad consumer reach (.NET Framework and older runtimes) |


## Repository layout

| Path | Purpose |
| --- | --- |
| `src/CLASP.NET/` | Main library (published to NuGet as `CLASP.NET`) |
| `tests/CLASP.NET.Tests/` | Unit tests |
| `samples/CLASP.NET.QuickStart/` | Minimal consumer example |


## Building from source

Requires the [.NET SDK](https://dotnet.microsoft.com/download) version
specified in [`global.json`](./global.json).

```bash
./build.sh        # Linux / macOS
./build.ps1       # Windows PowerShell
```

Or:

```bash
dotnet restore CLASP.NET.sln
dotnet build CLASP.NET.sln --configuration Release
dotnet test CLASP.NET.sln --configuration Release
dotnet pack src/CLASP.NET/CLASP.NET.csproj --configuration Release --output artifacts/packages
```


## Examples

See [EXAMPLES.md](./EXAMPLES.md).


## Project Information


### Where to get help

[GitHub Issues](https://github.com/synesissoftware/CLASP.NET/issues)


### Contribution guidelines

Defect reports, feature requests, and pull requests are welcome. See
[CONTRIBUTING.md](./CONTRIBUTING.md).


### Dependencies

None beyond the .NET BCL for the core parsing / usage / binding surface.


### Related projects

**CLASP.NET** is inspired by the [C/C++ CLASP library](https://github.com/synesissoftware/CLASP), which is documented in the articles:

* _An Introduction to CLASP_, Matthew Wilson, [CVu](http://accu.org/index.php/journals/c77/), January 2012;
* _[Anatomy of a CLI Program written in C](http://synesis.com.au/publishing/software-anatomies/anatomy-of-a-cli-program-written-in-c.html)_, Matthew Wilson, [CVu](http://accu.org/index.php/journals/c77/), September 2012; and
* _[Anatomy of a CLI Program written in C++](http://synesis.com.au/publishing/software-anatomies/anatomy-of-a-cli-program-written-in-c++.html)_, Matthew Wilson, [CVu](http://accu.org/index.php/journals/c77/), September 2015.

Other CLASP libraries include:

* [**CLASP**](https://github.com/synesissoftware/CLASP/)
* [**CLASP.Go**](https://github.com/synesissoftware/CLASP.Go/)
* [**CLASP.js**](https://github.com/synesissoftware/CLASP.js/)
* [**CLASP.Python**](https://github.com/synesissoftware/CLASP.Python/)
* [**CLASP.Ruby**](https://github.com/synesissoftware/CLASP.Ruby/)

Projects in which **CLASP.NET** is used include:

* [**libCLImate.NET**](https://github.com/synesissoftware/libCLImate.NET)


### License

**CLASP.NET** is released under the 3-clause BSD license. See
[LICENSE](./LICENSE) for details.


<!-- ########################### end of file ########################### -->

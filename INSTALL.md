# CLASP.NET - Installation and Use <!-- omit in toc -->


## Table of Contents <!-- omit in toc -->

- [Install the package](#install-the-package)
- [From a source checkout](#from-a-source-checkout)
- [Using the library](#using-the-library)


## Install the package

```bash
dotnet add package CLASP.NET
```


## From a source checkout

Requires the [.NET SDK](https://dotnet.microsoft.com/download) version specified in [`global.json`](./global.json).

```bash
dotnet restore CLASP.NET.sln
dotnet build CLASP.NET.sln --configuration Release
dotnet test CLASP.NET.sln --configuration Release
```

Or `./build.sh` (Linux / macOS) / `./build.ps1` (Windows PowerShell).


## Using the library

```csharp
using Clasp;
using Clasp.Util;
```

See [README.md](./README.md) for a Quick start, and [EXAMPLES.md](./EXAMPLES.md) for the sample program.


<!-- ########################### end of file ########################### -->

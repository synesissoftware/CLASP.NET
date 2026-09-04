# CLASP.NET - Contributing <!-- omit in toc -->

Thank you for your interest in contributing.


## Table of Contents <!-- omit in toc -->

- [Code of conduct](#code-of-conduct)
- [Reporting issues](#reporting-issues)
- [Getting started](#getting-started)
- [Project layout](#project-layout)
- [Coding standards](#coding-standards)
- [Documentation](#documentation)
- [Pull requests](#pull-requests)
- [Releases](#releases)
- [License](#license)


## Code of conduct

Be respectful and constructive. Defect reports, feature requests, and pull
requests are welcome on [GitHub](https://github.com/synesissoftware/CLASP.NET).


## Reporting issues

Use [GitHub Issues](https://github.com/synesissoftware/CLASP.NET/issues). Include:

* **CLASP.NET** version (NuGet package version or release tag);
* Target framework (`net8.0`, `netstandard2.0`, or consumer TFM);
* Operating system and architecture;
* .NET runtime or SDK version;
* Minimal reproduction steps or a link to a branch;

For build failures, attach the `dotnet build` / `dotnet test` log. For
behavioural issues, show expected vs actual results.


## Getting started

1. Install the [.NET SDK](https://dotnet.microsoft.com/download) version pinned in `global.json`;
2. Clone the repository and restore dependencies:

   ```bash
   dotnet restore CLASP.NET.sln
   ```

3. Build and test:

   ```bash
   dotnet build CLASP.NET.sln
   dotnet test CLASP.NET.sln
   ```

   Or use the cross-platform build scripts:

   ```bash
   ./build.sh        # Linux / macOS
   ./build.ps1       # Windows PowerShell
   ```


## Project layout

| Path | Purpose |
|------|---------|
| `src/CLASP.NET/` | Main library (published to NuGet as `CLASP.NET`) |
| `tests/CLASP.NET.Tests/` | Unit tests |
| `samples/CLASP.NET.QuickStart/` | Minimal consumer example |

Shared MSBuild settings live in `Directory.Build.props`. NuGet package
versions are centralized in `Directory.Packages.props`.


## Coding standards

Formatting and analyser settings are defined in [`.editorconfig`](.editorconfig)
and shared MSBuild properties in [`Directory.Build.props`](Directory.Build.props).
The editor ruler at column 76 in [`.vscode/settings.json`](.vscode/settings.json)
marks the documentation limit.

* **C#** sources use **4 spaces** per indent level (not tabs);
* Builds treat warnings as errors; nullable reference types are enabled for
  new code (the ported Framework sources currently override to nullable
  disable — see **TODO.md**);
* Match existing naming, layout, and brace style in the file you edit;
* Public API in **`src/CLASP.NET/`** must have XML documentation (`///`);
* Keep changes focused; prefer small, reviewable pull requests;


### `DOC_76` (the 76-rule for documentation comments)

All XML documentation comments (`///`) on **public** library members must
follow this convention:

1. **Line length**: no `///` line may exceed **76 characters** in total
   (including leading indentation, `///`, spaces, and XML markup). Code
   lines are not limited;
2. **Greedy wrapping**: pack as many words as possible onto each line up to
   that limit. Do not wrap early when the next word still fits;
3. **Capitalisation**: the first word of a comment should be capitalised,
   unless it is a well-known name (for example, `CLASP`, `NuGet`);

**Single-line form** — when the opening tag, text, and closing tag all fit
on one line:

```csharp
/// <summary>Major version component.</summary>
```

**Multi-line form** — when the single-line form would exceed 76 characters:

* the opening and closing tags each occupy their own line;
* prose is placed on separate lines between them;
* each prose line has **one extra leading space** after `///` (that is,
  `///  ` rather than `/// `);

```csharp
/// <summary>
///  Human-readable version string matching
///  <see cref="Major"/>.<see cref="Minor"/>.<see cref="Patch"/>.
/// </summary>
```

**Inline comments** (`//` inside methods) are limited to **100 characters**
per line (including indentation) and should be wrapped greedily.


## Documentation

When adding or changing behaviour, update as appropriate:

* [README.md](./README.md) — overview, building, and API summary;
* [CHANGES.md](./CHANGES.md) — version-first release notes;
* XML documentation on affected public members in **`src/CLASP.NET/`**;

Markdown body lists use `*` and each item ends with a **`;`**.


## Pull requests

1. Create a feature branch from `master` (or the agreed base branch);
2. Ensure `dotnet build` and `dotnet test` pass locally;
3. Update **CHANGES.md** when the change is user-visible;
4. Open a pull request with a clear description of the problem and solution;


## Releases

Maintainers publish releases by creating a GitHub Release. The release
workflow packs the library and publishes to
[NuGet.org](https://www.nuget.org/) when `NUGET_API_KEY` is configured in
repository secrets.

Tag releases with semantic version tags (for example, `0.27.0` or `v0.27.0`).


## License

By contributing, you agree that your contributions will be licensed under
the [3-clause BSD license](LICENSE) as the project.


<!-- ########################### end of file ########################### -->

# GitHub Copilot Instructions for microtype-generator

## Project Overview
- **microtype-generator** is a .NET source generator for microtypes.
- It provides a Roslyn incremental generator that generates strongly-typed wrappers (microtypes) around primitive types, using a `[MicroTypeAttribute<T>]` annotation.

## Main Components
- **Entry Point:** `MicroTypeIncrementalGenerator` in `src/MicroTypeGenerator.cs` implements `IIncrementalGenerator`.
- **Attribute:** The generator emits a `MicroTypeAttribute<T>` for marking types to be expanded as microtypes.
- **Source Generation:** For each type marked with `[MicroTypeAttribute<T>]`, the generator emits a partial class/struct that wraps the primitive type `T` and implements equality, conversion, and utility methods.

## Usage
- Reference this package as an analyzer in your .NET project.
- Annotate your partial class or struct with `[MicroTypeAttribute<T>]`, where `T` is the primitive type to wrap.
- The generator will emit the microtype implementation at compile time.

## Build Instructions
- Build with: `dotnet build src/MicrotypeGenerator.csproj`
- Restore dependencies: `dotnet restore src/MicrotypeGenerator.csproj`

## Code Style
- Follows .NET and C# conventions as enforced by `.editorconfig`.
- Indentation: 4 spaces, UTF-8, LF line endings.
- Naming: PascalCase for types/members, camelCase for locals/parameters, _camelCase for private fields.

## Contribution Guidelines
- Follow the code style and naming conventions.
- Add or update tests for new features or bug fixes (if/when tests are present).
- Use clear, descriptive commit messages.
- Ensure builds pass before submitting PRs.

## Testing
- (No test project present in this repo as of this writing.)
- If you add tests, use standard .NET test frameworks (e.g., xUnit, NUnit, MSTest).

## Dependencies
- Managed in `Directory.Packages.props`

## Configuration
- No runtime configuration required.
- Generator is packaged as a development dependency and analyzer.

## License
- MIT License (see LICENSE file).

## Additional Notes
- This project is intended for use as a source generator/analyzer, not as a runtime library.
- For more details, see the `README.md` and source code in `src/`.
- Must: Use dotnet CLI for building and managing the project.
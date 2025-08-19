# JSON-CS Project

Port of JSON Parser from TypeScript to .NET 8.0 and C# 12

## License

MIT

## Reference

[json.org](http://json.org)

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Build

```sh
dotnet build
```

Builds all projects in the solution.

## Format

```sh
dotnet format
```

Formats code to match .NET style conventions.

## Lint

```sh
dotnet format --verify-no-changes
```

Checks code formatting and reports any style violations.

## Test

```sh
dotnet test
```

Runs all unit tests in the solution.

## Project Structure

- `CLI/` - Command-line interface
- `Shared/` - Core library and parsers
- `Shared.Tests/` - Unit tests

## Usage

### Run CLI with user input

```sh
dotnet run --project CLI
```

You will be prompted to enter JSON data directly in the terminal.

### Run CLI with file input piped

```sh
type input.json | dotnet run --project CLI
```

Or, on Unix-like systems:

```sh
cat input.json | dotnet run --project CLI
```

This will read JSON data from `input.json` and process it via the CLI.

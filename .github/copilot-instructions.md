# Copilot Instructions for JSON-CS

## Project Overview

- **Purpose:** Port of a TypeScript JSON parser to .NET 8.0 (C# 12).
- **Major Components:**
  - `CLI/`: Command-line interface for parsing JSON input.
  - `Shared/`: Core library, including token models and parser logic.
    - `Models/`: JSON token types (e.g., `ArrayToken`, `ObjectToken`, etc.).
    - `Parsers/`: Parsers for each JSON structure (e.g., `Array.cs`, `Object.cs`).
  - `Shared.Tests/`: Unit tests for core library and parsers.

## Developer Workflows

- **Build:** `dotnet build` (builds all projects)
- **Format:** `dotnet format` (auto-formats code)
- **Lint:** `dotnet format --verify-no-changes` (checks for style violations)
- **Test:** `dotnet test` (runs all unit tests)
- **Run CLI:**
  - Interactive: `dotnet run --project CLI`
  - File input: `type input.json | dotnet run --project CLI`

## Patterns & Conventions

- **Parser Design:** Each JSON type (array, object, string, etc.) has a dedicated parser in `Shared/Parsers/` and a corresponding token in `Shared/Models/`.
- **Token Model:** All token types inherit from `Token.cs` for polymorphic handling.
- **Testing:** Each parser has a matching test file in `Shared.Tests/Parsers/` (e.g., `ArrayTests.cs` for `Array.cs`).
- **No external dependencies** beyond .NET SDK; all parsing logic is custom.
- **CLI** uses `Shared.dll` for all parsing operations.

## Integration Points

- **CLI <-> Shared:** CLI project references `Shared` for parsing logic.
- **Tests <-> Shared:** Tests directly exercise parser and token logic from `Shared`.

## Examples

- To add a new JSON type, create a token in `Shared/Models/` and a parser in `Shared/Parsers/`, then add tests in `Shared.Tests/Parsers/`.
- To debug parsing, run CLI interactively and inspect output/errors.

## Key Files

- `Shared/JSON.cs`: Entry point for parsing logic.
- `Shared/Models/Token.cs`: Base class for all tokens.
- `Shared/Parsers/Value.cs`: Central parser dispatch for JSON values.

---

**For questions or unclear conventions, review `README.md` or ask for clarification.**

---

## API Project (Added in Recent Updates)

- The solution now includes an `API/` project using ASP.NET Core minimal API.
- Main endpoint: `POST /api/v1/parse`
  - Accepts a JSON body, parses it using the Shared library, and returns the result as JSON.
  - Errors are returned as JSON objects with a message and code.
- To run the API server:
  ```sh
  dotnet run --project API/API.csproj
  ```
- To run the API server with automatic restart on code changes:
  ```sh
  dotnet watch run --project API/API.csproj
  ```
- You can test the API using the REST Client VS Code extension and `.rest` files in `API/testdata/`.

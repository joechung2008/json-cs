# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## 2025-10-02

- Add .vscode/tasks.json
- Replace coverlet.collector with Microsoft's built-in code coverage

## 2025-10-01

- Add .dockerignore
- Move test data to /testdata

## 2025-09-29

- Add Dockerfile to API and CLI projects

## 2025-09-28

- Address Rider warnings

## 2025-09-10

- Fix stdin processing in CLI
- Remove leftover cruft
- Update outdated packages

## 2025-09-06

- Be less specific about .NET 8 version

## 2025-09-02

- Update LICENSE

## 2025-08-24

- Add Content-Length headers to .rest files and support for OpenAPI 2.0 and 3.0
- Move ReadToEnd inside try-catch
- Remove trailing whitespace from CLI input and unused API.http file
- Update copilot-instructions.md

## 2025-08-20

- Add API project with ASP.NET Web API implementation
- Fix MIME types in testdata and example request
- Format example request JSON
- Recommend VS Code extensions
- Update API to accept text/plain

## 2025-08-19

- Initial commit
- Add LICENSE
- Handle parsing whitespace after numbers
- Update README.md

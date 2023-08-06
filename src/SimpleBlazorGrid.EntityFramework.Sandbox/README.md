# Entity Framework Sandbox

Use this project to test the Simple Blazor Grid against Entity Framework

## Installation

This project uses SQLite as the data store, A file named `simpleblazorgrid.db` will be created in your local apps folder, you may need to change this if you're not running on Windows.

Additionally, you will need the Entity Framework CLI tool to apply the migrations.

1. Install the tools with
```dotnet tool install --global dotnet-ef```
2. Run the migrations from this projects directory with
```dotnet ef database update```
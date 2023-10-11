# Simple Blazor Grid

> ⚠ Simple Blazor Grid is still in prelease, some features may be missing or broken ⚠

Simple Blazor Grid provides a simple and effective way to easily create data grids in Blazor with searching, sorting, paging and filtering all supported out-of-the-box.

## [Website](https://simpleblazorgrid.com) | [Documentation](https://simpleblazorgrid.com/documentation) | [NuGet](https://www.nuget.org/packages/SimpleBlazorGrid.Core)

![A screenshot of a Simple Blazor Grid](img/screenshot_table.png)

## Getting Started

### 1. Install Simple Blazor Grid into your project from [NuGet](https://www.nuget.org/packages/SimpleBlazorGrid.Core)

```
dotnet add package SimpleBlazorGrid.Core
```

### 2. Add Simple Blazor Grid to your services

```
builder.Services.AddSimpleBlazorGrid();
```

You can additionally, pass a `SimpleDataGridConfiguration` object to `.AddSimpleBlazorGrid()` to configure table colours and data formatting. For a full rundown, see the [configuration](https://simpleblazorgrid.com/documentation/configuration) section of the documentation.

### 3. Import the CSS

In your `_Host.cshtml` file, add the following line;

```
<link rel="stylesheet" href="~/_content/SimpleBlazorGrid.Core/css/simpledatagrid.css"/>
```

### 4. Create your first Simple Blazor Grid!

_Congratulations!_ You are now ready to create your first grid. Take a look at the [demo grid](https://simpleblazorgrid.com) on the home page to see how Simple Blazor Grid workd or read the [getting started](https://simpleblazorgrid.com/documentation/getting-started) guide to get started!

## Building

SASS is required to build the solution, please run the appropriate command below to install it.

### Windows (Chocolatey)

```
choco install sass
```

### MacOS / Linux (Homebrew)

```
brew install sass/sass/sass
```

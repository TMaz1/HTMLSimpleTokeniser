# HTML Simple Tokeniser

A simple tokeniser that parses a HTML file into **tokens**, as a C# console application.

The tokeniser currently identifies:
- **Start Tags** (e.g., `<div>`): Identified as `+TAG` in the console.
- **End Tags** (e.g., `</div>`): Identified as `-TAG` in the console.
- **Text Content** (e.g., `Text` within a tag): Identified as `TEXT` in the console.

Tokenisation is the first step process a HTML parser takes to interpret HTML to a structure (DOM) that the browser can use to render a webpage.

## Project Relevance

This project was created to explore the fundamentals of HTML tokenisation and how browsers begin processing raw HTML into structured data. The tokeniser identifies basic HTML components such as start tags, end tags, and text content, demonstrating the first stage of the parsing process used by browsers and parsers.

The project is intended as a learning exercise focused on:

- Tokenisation and lexical analysis
- Basic parser design concepts
- String and character processing
- State-based logic
- Unit and integration testing in C#

It also provides a foundation for extending the project into a more complete HTML parser in the future. E.g., Handle attributes like `class` and `id` and/or expand token types to all possible tokens: `DOCTYPE`, `start tag`, `end tag`, `comment`, `character`, and `end-of-file`.

## Features

-  **File Reading**: `HtmlTokeniser` class reads the HTML file into a string (specified in `appsettings.json`), if the file is not available, a fallback HTML file is created.
-  **Token Recognition**: The tokeniser identifies:
	-  **Start tags**: Detected by `<` and processed until the closing `>`.
	-  **End tags**: Detected by `</` and processed until the closing `>`.
	-  **Text content**: Characters outside of tags.
	`position` tracks the current character. If a `<` is found, it is identified as a *start tag* or *end tag*. If normal characters, whitespace removed, are found (within tags), it is identified as *text content*.
-  **Output Tokens**: Each token is created with a Type (*start tag, text content, end tag*) and Value (e.g., `div class="product"`, `Product Name`, `div`, respectively).
-  **Logger**: Singleton logger (logs processes/errors).
- **Testing**: unit and integration testing with xUnit

## Setup

**Clone the Repo:**
```
git clone https://github.com/TMaz1/HTMLSimpleTokeniser.git
```

**Run Project:**
```
cd HtmlSimpleTokeniser
dotnet run
```

**Run Test Project:**
```
cd HtmlSimpleTokeniserTests
dotnet test
```

# Task: Build a Modern cURL Debugger Visualizer for Visual Studio (ikeskin/CurlDebuggerVisualizer2)

## Context
This project aims to create a modern and user-friendly Visual Studio Debugger Visualizer named **CurlDebuggerVisualizer2**, to inspect `HttpResponseMessage` objects with a professional UI experience.

Inspired by tools like Sentry and Postman, this visualizer will provide developers with the ability to:
- See the equivalent cURL command for any HTTP request
- View the full raw HTTP response (status line, headers, body)
- View a pretty-formatted JSON response (if applicable), with collapsible tree view (future version)

## Requirements

### Project Setup
- Project Name: `CurlDebuggerVisualizer2`
- Target Framework: `.NET Framework 4.8`
- Project type: **WPF Class Library**  
  Reason: to support modern UI via `WebView2` and WPF styling
- Dependencies:
    - Reference: `Microsoft.VisualStudio.DebuggerVisualizers.dll`
    - NuGet Package: `Microsoft.Web.WebView2`

### Core Functionalities

✅ Implement `DialogDebuggerVisualizer`:
- Target type: `HttpResponseMessage`
- In `Show()` method:
    - Extract `HttpResponseMessage` via `IVisualizerObjectProvider3.GetObject<HttpResponseMessage>()`
    - Build:
        - cURL command string
        - Raw HTTP response string
        - Pretty JSON string (if Content-Type is `application/json`)

✅ Implement `CurlResponseVisualizerForm` (WPF Window):
- Contains a `WebView2` control
- Loads an HTML template with:
    - Bootstrap-based tabbed UI:
        - **Tab 1:** cURL Command
        - **Tab 2:** Raw Response
        - **Tab 3:** Pretty JSON (initially basic, collapsible in later commit)
    - Simple Copy-to-Clipboard buttons per tab (planned for later commit)

✅ HTML Template:
- `Assets/template.html`:
    - Bootstrap 5 for layout
    - Highlight.js for JSON syntax highlighting (planned for later commit)
    - Placeholders:
        - `{{CURL_COMMAND}}`
        - `{{RAW_RESPONSE}}`
        - `{{PRETTY_JSON}}`
    - Placeholders dynamically replaced in C# → injected into WebView2

✅ `CurlHelper.cs` class:
- `ToCurlCommand(HttpRequestMessage request)`
- `ToRawResponse(HttpResponseMessage response)`
- `ToPrettyJson(string json)`

✅ README.md:
- How to build the project
- How to deploy:
    - Copy `CurlDebuggerVisualizer2.dll` to:
    ```
    %USERPROFILE%\Documents\Visual Studio 2022\Visualizers\
    ```
- How to test:
    - Debug a project with `HttpResponseMessage`
    - Use magnifier icon (Debugger Visualizer) to launch CurlDebuggerVisualizer2

## Commit Strategy

### First Commit
- Functional version:
    - cURL tab working
    - Raw Response tab working
    - Pretty JSON tab working (non-collapsible)

### Second Commit (Planned)
- Enhance UI:
    - Collapsible JSON using Highlight.js or json-viewer
    - Copy-to-Clipboard buttons
    - Improved styling
    - Dark mode support (optional)

## Constraints
- Solution must build successfully with Visual Studio 2022.
- First commit must provide a working end-to-end experience.
- Second commit focuses on enhanced UX.

## Goals
Provide a **professional, modern Debugger Visualizer** that gives Visual Studio users a better way to inspect `HttpResponseMessage` objects, helping developers debug REST APIs effectively.

---

# Example Usage:

**When debugging in Visual Studio:**

1. Place breakpoint after an `HttpClient` call returning an `HttpResponseMessage`.
2. Hover over the variable or add to Watch window.
3. Click the magnifier (Debugger Visualizer).
4. CurlDebuggerVisualizer2 will open a window displaying:
    - cURL Command
    - Raw HTTP Response
    - Pretty JSON view (if applicable)

---

# Repository Structure:

CurlDebuggerVisualizer2
├── CurlDebuggerVisualizer.sln
├── CurlDebuggerVisualizer (WPF Class Library)
│ ├── CurlResponseVisualizer.cs
│ ├── CurlResponseVisualizerForm.xaml / .cs
│ ├── CurlHelper.cs
│ ├── Assets/template.html
├── README.md
└── PROMPT.md (this file)


---

# Final Note
This project is designed to serve as a reusable template for other Debugger Visualizers in Visual Studio, combining modern UI with practical debugging utilities.

---

**Contributed by:**  
[@ikeskin](https://github.com/ikeskin)  
(Generated task prompt template by AI assistant to capture project goals.)

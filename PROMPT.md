# Task: Build a modern cURL Debugger Visualizer for Visual Studio

## Context
We want to build a modern, user-friendly Debugger Visualizer for Visual Studio (2022), which allows developers to inspect `HttpResponseMessage` objects in a friendly UI:
- It should display:
    - A generated `cURL` command equivalent for the request
    - The full raw HTTP response (headers + body)
    - A pretty-formatted JSON response (if applicable)

## Requirements

1️⃣ Create a Visual Studio Debugger Visualizer project:
- Target Framework: `.NET Framework 4.8`
- Project type: WPF Class Library
- Dependencies:
    - `Microsoft.VisualStudio.DebuggerVisualizers.dll` (reference)
    - NuGet: `Microsoft.Web.WebView2` (latest stable)

2️⃣ Implement a `DialogDebuggerVisualizer`:
- Target type: `HttpResponseMessage`
- In `Show()` method:
    - Extract `HttpResponseMessage` via `IVisualizerObjectProvider3.GetObject<HttpResponseMessage>()`
    - Build:
        - `cURL` string (HttpRequestMessage → cURL)
        - Raw Response string (Status line + Headers + Body)
        - Pretty JSON (if Content-Type is `application/json`)

3️⃣ Implement a WPF Window (Dialog):
- Name: `CurlResponseVisualizerForm`
- Contains a `WebView2` control
- Load an HTML template with:
    - Bootstrap-based tabbed UI:
        - Tab 1: cURL Command
        - Tab 2: Raw Response
        - Tab 3: Pretty JSON
    - Simple Copy buttons (JS-based)

4️⃣ HTML template:
- Use Bootstrap 5 for tabs
- Use Highlight.js for JSON syntax highlighting (collapsible not required for first version)
- Include placeholder tokens:
    - `{{CURL_COMMAND}}`
    - `{{RAW_RESPONSE}}`
    - `{{PRETTY_JSON}}`
- Replace placeholders dynamically via C# when injecting content into WebView2.

5️⃣ Create a `CurlHelper.cs` class:
- `ToCurlCommand(HttpRequestMessage request)` → generate equivalent cURL command as string
- `ToRawResponse(HttpResponseMessage response)` → generate full HTTP response as string
- `ToPrettyJson(string json)` → pretty-print JSON (using Newtonsoft.Json)

6️⃣ Prepare a README.md:
- Explain how to build the project
- How to deploy to `%USERPROFILE%\Documents\Visual Studio 2022\Visualizers\`
- How to test in Visual Studio (debug a project with `HttpResponseMessage`, use magnifier icon to launch visualizer)

7️⃣ Stretch goal (optional, later commit):
- Add collapsible JSON with Highlight.js or json-viewer
- Add Copy-to-Clipboard buttons per tab
- Add dark mode theme

## Constraints
- Initial version must be functional and buildable with standard Visual Studio 2022 + WebView2 + WPF setup.
- The first commit should implement a basic working version without collapsible JSON or advanced styling.
- Second commit can enhance the UI.

## Output
- A GitHub repo structured as:
    - `CurlDebuggerVisualizer.sln`
    - `CurlDebuggerVisualizer` project with:
        - `CurlResponseVisualizer.cs`
        - `CurlHelper.cs`
        - `CurlResponseVisualizerForm.xaml` + `.cs`
        - `Assets/template.html`
    - `README.md`

## Goal
Provide developers with a modern, professional Debugger Visualizer for HttpResponseMessage objects, similar to Sentry or Postman UI quality.

---

# Example Input:
An HttpResponseMessage captured during debug.

# Example Output:
A modern WPF Window with WebView2:
- cURL tab → ready to copy
- Raw Response tab → readable HTTP dump
- Pretty JSON tab → formatted JSON if applicable

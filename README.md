# CurlDebuggerVisualizer2

A Visual Studio debugger visualizer to inspect `HttpResponseMessage` objects with a modern UI using WPF and WebView2. JSON responses are rendered with syntax highlighting via **highlight.js** inside a WebView2 control.

## Building

Open `CurlDebuggerVisualizer.sln` with Visual Studio 2022 and restore NuGet packages. The project targets **.NET Framework 4.8** and requires the **WebView2** runtime.

## Deploying

After building the project, copy `CurlDebuggerVisualizer2.dll` from the `bin/Release` folder to:

```
%USERPROFILE%\Documents\Visual Studio 2022\Visualizers\
```

Restart Visual Studio if it was running.

## Testing

1. Create or open a project that performs an HTTP request using `HttpClient`.
2. Place a breakpoint after receiving the `HttpResponseMessage`.
3. Start debugging and when the breakpoint hits, hover over the variable or add it to the Watch window.
4. Click the magnifier icon to launch the **CurlDebuggerVisualizer2** window.
5. Inspect the cURL command, raw response, and syntax-highlighted JSON in the visualizer.

## Repository Structure

```
CurlDebuggerVisualizer2
├── CurlDebuggerVisualizer.sln
├── CurlDebuggerVisualizer (WPF Class Library)
│   ├── Assets
│   │   └── template.html
│   ├── CurlHelper.cs
│   ├── CurlResponseVisualizer.cs
│   ├── CurlResponseVisualizerForm.xaml
│   └── CurlResponseVisualizerForm.xaml.cs
├── README.md
└── PROMPT.md
```

This repository provides a simple but functional starting point for creating modern debugger visualizers for Visual Studio.

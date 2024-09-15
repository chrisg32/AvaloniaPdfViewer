![Avalonia PDF Viewer](./icon.png)
# Avalonia PDF Viewer: A PDF viewer for Avalonia UI

![GitHub License](https://img.shields.io/github/license/chrisg32/AvaloniaPdfViewer)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/chrisg32/AvaloniaPdfViewer/ci)
[![Nuget](https://img.shields.io/nuget/v/AvaloniaPdfViewer)](https://www.nuget.org/packages/AvaloniaPdfViewer/)
![NuGet Downloads](https://img.shields.io/nuget/dt/AvaloniaPdfViewer)

**Avalonia PDF Viewer** in an [Avalonia UI](https://avaloniaui.net) control for viewing [PDF files](https://en.wikipedia.org/wiki/PDF).

## Features

- Cross-platform (Windows, macOS, Linux)
- PDF rendering using [PDFium](https://pdfium.googlesource.com/pdfium/) and [SkiaSharp](https://github.com/mono/SkiaSharp).
- Zooming and panning
- Page navigation
- Thumbnail view

## 🚀 Getting Started

Install the [AvaloniaPdfViewer NuGet package](https://www.nuget.org/packages/AvaloniaPdfViewer/).

```bash
dotnet add package AvaloniaPdfViewer
```

In your XAML file, add the following namespace:

```xml
xmlns:avaloniaPdfViewer="clr-namespace:AvaloniaPdfViewer;assembly=AvaloniaPdfViewer"
```

Add the `PdfViewer` control to your XAML file.

```xml
<avaloniaPdfViewer:PdfViewer Source="{Binding FilePath}"/>
```

## Source

This library is built on top of these great open source projects.

- [Avalonia](https://github.com/AvaloniaUI/Avalonia) (cross-platform UI framework for dotnet)
- [PdfToImage](https://github.com/sungaila/PDFtoImage) (A .NET library to render PDF files into images.)
  - [PDFium](https://pdfium.googlesource.com/pdfium/) (native PDF renderer)
  - [SkiaSharp](https://github.com/mono/SkiaSharp) (cross-platform 2D graphics API)
- [material-design-icons](https://github.com/google/material-design-icons) (Google font icons)

## License

Avalonia PDF Viewer is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

HOWEVER, this project uses dependencies that are licensed under other licenses. Please check the licenses of the dependencies listed above before using this project.
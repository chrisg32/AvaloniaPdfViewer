using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

namespace AvaloniaPdfViewer;

public partial class PdfViewer : UserControl
{
    public PdfViewer()
    {
        InitializeComponent();
    }
    
    private PdfViewerViewModel? ViewModel => VisualChildren[0].DataContext as PdfViewerViewModel;
    
    public static readonly DirectProperty<PdfViewer, string?> SourceProperty =
        AvaloniaProperty.RegisterDirect<PdfViewer, string?>(nameof(Source), o => o.Source, (o, v) => o.Source = v);

    private string? _source;
    public string? Source
    {
        get => _source;
        set => SetAndRaise(SourceProperty, ref _source, value);
    }
}
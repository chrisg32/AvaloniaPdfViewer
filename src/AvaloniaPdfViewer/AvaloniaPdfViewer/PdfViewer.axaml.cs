using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

namespace AvaloniaPdfViewer;

public partial class PdfViewer : UserControl
{
    private PdfViewerViewModel? ViewModel => DataContext as PdfViewerViewModel;
    public PdfViewer()
    {
        DataContext = new PdfViewerViewModel();
        InitializeComponent();
    }
    
    // public static readonly DirectProperty<PdfViewer, IFileSource> SourceProperty =
    //     AvaloniaProperty.RegisterDirect<PdfViewer, IFileSource>(nameof(Source), p => p.Source);
    //
    // public IFileSource Source
    // {
    //     get => GetValue(SourceProperty);
    //     set => SetValue(SourceProperty, value);
    // }
    
    public static readonly StyledProperty<string?> SourceProperty =
        AvaloniaProperty.Register<PdfViewer, string?>(nameof(Source));
    
    public string? Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
}
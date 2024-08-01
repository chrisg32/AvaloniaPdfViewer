using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaPdfViewer;

public partial class PdfViewer : UserControl
{
    private PdfViewerViewModel? ViewModel => DataContext as PdfViewerViewModel;
    public PdfViewer()
    {
        DataContext = new PdfViewerViewModel();
        InitializeComponent();
    }
}
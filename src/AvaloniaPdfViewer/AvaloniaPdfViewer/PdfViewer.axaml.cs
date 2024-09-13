using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaPdfViewer.Internals;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using SkiaSharp;
using PdfConvert = PDFtoImage.Conversion;

namespace AvaloniaPdfViewer;

public partial class PdfViewer : UserControl
{
    public static readonly DirectProperty<PdfViewer, string?> SourceProperty =
        AvaloniaProperty.RegisterDirect<PdfViewer, string?>(nameof(Source), o => o.Source, (o, v) => o.Source = v);

    static PdfViewer()
    {
        SourceProperty.Changed.AddClassHandler<PdfViewer>((x, e) => x.Load(e));
    }
    public PdfViewer()
    {
        _thumbnailImagesCache.Connect()
            .Sort(SortExpressionComparer<DrawableThumbnailImage>.Ascending(i => i.Index))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _thumbnailImages)
            .DisposeMany()
            .Subscribe();
        
        InitializeComponent();
    }

    private string? _source;
    public string? Source
    {
        get => _source;
        set => SetAndRaise(SourceProperty, ref _source, value);
    }
    
    public int ThumbnailCacheSize { get; set; } = 10;
    
    private readonly SourceCache<DrawableThumbnailImage, int> _thumbnailImagesCache = new(i => i.PageNumber);
    private readonly ReadOnlyObservableCollection<DrawableThumbnailImage> _thumbnailImages;
    private ReadOnlyObservableCollection<DrawableThumbnailImage> ThumbnailImages => _thumbnailImages;

    public void Load(AvaloniaPropertyChangedEventArgs args)
    {
        var source = args.NewValue as string;
        if(string.IsNullOrWhiteSpace(source) || !File.Exists(source))
        {
            _thumbnailImagesCache.Clear();
            MainImage.Source = null;
            PageSelector.Maximum = 0;
            PageCount.Text = "0";
            return;
        }
        
        var fileStream = File.OpenRead(source);
        
        var doc =  PdfDocumentReflection.Load(fileStream, null, false);
        var sizes = PdfDocumentReflection.PageSizes(doc);
        
        PageSelector.Maximum = sizes.Count;
        PageCount.Text = sizes.Count.ToString();
        
        var thumbnailCache = new DisposingLimitCache<int, SKBitmap>(ThumbnailCacheSize);
        
        _thumbnailImagesCache.Edit(edit =>
        {
            edit.Load(sizes.Select((size, index) => new DrawableThumbnailImage(size, fileStream, index, thumbnailCache)));
        });
        
        var skbitMap = PdfConvert.ToImage(fileStream, new Index(0), leaveOpen: true);
        
        MainImage.Source = skbitMap.ToAvaloniaImage();
    }

    private void ThumbnailSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }
}
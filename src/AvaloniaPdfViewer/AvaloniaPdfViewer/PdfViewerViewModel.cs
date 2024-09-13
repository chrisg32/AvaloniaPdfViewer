using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Media;
using AvaloniaPdfViewer.Internals;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using SkiaSharp;
using PdfConvert = PDFtoImage.Conversion;

namespace AvaloniaPdfViewer;

internal class PdfViewerViewModel : ReactiveObject
{
    private readonly SourceCache<DrawableThumbnailImage, int> _thumbnailImagesCache = new(i => i.PageNumber);
    private ReadOnlyObservableCollection<DrawableThumbnailImage> _thumbnailImages;
    public ReadOnlyObservableCollection<DrawableThumbnailImage> ThumbnailImages => _thumbnailImages;
    public IImage? Image { get; private set; }
    public int PageCount { get; set; }
    public PdfViewerViewModel()
    {
        _thumbnailImagesCache.Connect()
            .Sort(SortExpressionComparer<DrawableThumbnailImage>.Ascending(i => i.Index))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _thumbnailImages)
            .DisposeMany()
            .Subscribe();
    }

    public void Load(string? source)
    {
        if(string.IsNullOrWhiteSpace(source) || !File.Exists(source))
        {
            _thumbnailImagesCache.Clear();
            Image = null;
            PageCount = 0;
            return;
        }
        
        var fileStream = File.OpenRead(source);
        
        var doc =  PdfDocumentReflection.Load(fileStream, null, false);
        var sizes = PdfDocumentReflection.PageSizes(doc);
        
        PageCount = sizes.Count;
        
        var thumbnailCache = new DisposingLimitCache<int, SKBitmap>(GlobalSettings.ThumbnailCacheSize);
        
        _thumbnailImagesCache.Edit(edit =>
        {
            edit.Load(sizes.Select((size, index) => new DrawableThumbnailImage(size, fileStream, index, thumbnailCache)));
        });
        
        var skbitMap = PdfConvert.ToImage(fileStream, new Index(0), leaveOpen: true);
        
        Image = skbitMap.ToAvaloniaImage();
    }
}
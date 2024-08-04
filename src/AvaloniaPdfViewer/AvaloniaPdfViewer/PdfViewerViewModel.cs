using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Media;
using AvaloniaPdfViewer.Internals;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using SkiaSharp;

namespace AvaloniaPdfViewer;

using PdfConvert = PDFtoImage.Conversion;
internal class PdfViewerViewModel
{
    private ReadOnlyObservableCollection<DrawableThumbnailImage> _thumbnailImages;
    public ReadOnlyObservableCollection<DrawableThumbnailImage> ThumbnailImages => _thumbnailImages;
    public IImage? Image { get; private set; }
    public int PageCount { get; set; }
    public PdfViewerViewModel()
    {
        var cache = new SourceCache<DrawableThumbnailImage, int>(i => i.PageNumber);

        cache.Connect()
            .Sort(SortExpressionComparer<DrawableThumbnailImage>.Ascending(i => i.Index))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _thumbnailImages)
            .DisposeMany()
            .Subscribe();
        
        const string filePath = "../../../../pdfreference1.0.pdf";
        
        var fileStream = File.OpenRead(filePath);

        Task.Delay(10000).Wait();

        // cache.Edit(edit =>
        // {
        //     edit.Load(PdfConvert.ToImages(fileStream, leaveOpen: true).Select((bitmap, index) => new ThumbnailImage{ PageNumber = index + 1, Image = bitmap.ToAvaloniaImage()}));
        // });
        
        // Image = ThumbnailImages.First().Image;
        var doc =  PdfDocumentReflection.Load(fileStream, null, false);
        var sizes = PdfDocumentReflection.PageSizes(doc);

        PageCount = sizes.Count;
        
        var thumbnailCache = new DisposingLimitCache<int, SKBitmap>(GlobalSettings.ThumbnailCacheSize);

        cache.Edit(edit =>
        {
            edit.Load(sizes.Select((size, index) => new DrawableThumbnailImage(size, fileStream, index, thumbnailCache)));
        });
        

        var skbitMap = PdfConvert.ToImage(fileStream, new Index(0), leaveOpen: true);

        // var memoryStream = skbitMap.Encode(SKEncodedImageFormat.Png, 100).AsStream();
        // Image = new Bitmap(memoryStream);

        Image = skbitMap.ToAvaloniaImage();
    }

    
}
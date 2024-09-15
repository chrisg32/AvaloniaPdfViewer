using System.Diagnostics;
using System.Drawing;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;
using Point = Avalonia.Point;
using Size = Avalonia.Size;
using PdfConvert = PDFtoImage.Conversion;

namespace AvaloniaPdfViewer.Internals;

//todo for later, make reactive and load images in background thread
internal class DrawableThumbnailImage(SizeF size, Stream pdfStream, int index, DisposingLimitCache<int, SKBitmap> cache) : IImage, IDisposable
{
    private readonly CachingSkBitmapDrawOperation _drawOperation = new(index, pdfStream, cache);
    public int Index { get; } = index;

    public int PageNumber => Index + 1;
    //todo handle in ui, being lazy right now
    public string PageNumberText => $"Page {PageNumber}";

    public void Draw(DrawingContext context, Rect sourceRect, Rect destRect)
    {
        _drawOperation.Bounds = destRect;
        context.Custom(_drawOperation);
    }

    public Size Size { get; } = new(size.Width, size.Height);

    public void Dispose()
    {
        cache.RemoveKey(Index);
    }
    
    private sealed class CachingSkBitmapDrawOperation(int index, Stream pdfStream, DisposingLimitCache<int, SKBitmap> cache) : ICustomDrawOperation
    {
        

        public bool Equals(ICustomDrawOperation? other) => ReferenceEquals(this, other);

        public void Dispose()
        {
            //do nothing
        }

        public bool HitTest(Point p) => Bounds.Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            Debug.Write($"Rendering thumbnail {index}...");
            if(!cache.TryGetValue(index, out var skBitmap))
            {
                Debug.Write("NOT found in cache, loading...");
#pragma warning disable CA1416
                skBitmap = PdfConvert.ToImage(pdfStream, new Index(index), leaveOpen: true);
#pragma warning restore CA1416
                cache.Add(index, skBitmap);
            }
            else
            {
                Debug.Write("found in cache...");
            }
            if (context.PlatformImpl.GetFeature<ISkiaSharpApiLeaseFeature>() is { } leaseFeature)
            {
                ISkiaSharpApiLease lease = leaseFeature.Lease();
                using (lease)
                {
                    lease.SkCanvas.DrawBitmap(skBitmap, SKRect.Create((float)Bounds.X, (float)Bounds.Y, (float)Bounds.Width, (float)Bounds.Height));
                }
            }
            Debug.WriteLine("done.");
        }

        public Rect Bounds { get; set; }
    }
}
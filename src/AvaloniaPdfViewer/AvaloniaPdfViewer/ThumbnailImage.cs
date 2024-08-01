using Avalonia;
using Avalonia.Media;
using DynamicData;
using SkiaSharp;

namespace AvaloniaPdfViewer;

//todo for later, thinking about how to virtualize rather than loading all images at once
// internal class DrawableThumbnailImage : IImage
// {
//     public int Index { get; }
//     public int PageNumber => Index + 1;
//
//     public DrawableThumbnailImage(SKBitmap source, Stream pdfStream, int index, SourceCache<SKBitmap, int> cache)
//     {
//         Index = index;
//     }
//
//     public void Draw(DrawingContext context, Rect sourceRect, Rect destRect)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Size Size { get; }
// }

public record ThumbnailImage
{
    //todo handle in ui, being lazy right now
    public string PageNumberText => $"Page {PageNumber}";
    public int PageNumber { get; init; }
    public IImage Image { get; init; }
}
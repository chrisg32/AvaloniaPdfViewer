using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using SkiaSharp;

namespace AvaloniaPdfViewer;
using PDFtoImage;
using PdfConvert = PDFtoImage.Conversion;
internal class PdfViewerViewModel
{
    
    public IImage? Image { get; private set; }
    public PdfViewerViewModel()
    {
        const string filePath = "../../../../pdfreference1.0.pdf";
        
        using var fileStream = File.OpenRead(filePath);

        var skbitMap = PdfConvert.ToImage(fileStream, new Index(0), leaveOpen: false);
        
        // var memoryStream = skbitMap.Encode(SKEncodedImageFormat.Png, 100).AsStream();
        // Image = new Bitmap(memoryStream);
        
        Image = skbitMap.ToAvaloniaImage();
    }

    
}
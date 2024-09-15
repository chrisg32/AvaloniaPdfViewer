using System.Drawing;
using System.Reflection;

namespace AvaloniaPdfViewer.Internals;

//https://github.com/sungaila/PDFtoImage/issues/93#issuecomment-2264905557
internal static class PdfDocumentReflection
{
    private static readonly Type PdfDocumentType = typeof(PDFtoImage.Conversion).Assembly.GetType("PDFtoImage.Internals.PdfDocument")!;

    private static readonly MethodInfo LoadMethod = PdfDocumentType.GetMethod("Load", BindingFlags.Public | BindingFlags.Static)!;

    private static readonly PropertyInfo PageSizesProperty = PdfDocumentType.GetProperty("PageSizes", BindingFlags.Public | BindingFlags.Instance)!;

    public static object Load(Stream stream, string? password, bool disposeStream) => LoadMethod.Invoke(null, [stream, password, disposeStream])!;

    public static IList<SizeF> PageSizes(object pdfDocument) => (IList<SizeF>)PageSizesProperty.GetValue(pdfDocument)!;
}
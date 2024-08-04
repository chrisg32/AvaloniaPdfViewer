using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;

namespace AvaloniaPdfViewer;

public interface IFileSource
{
    Stream Open();
}


public class StringFileSource(string path) : IFileSource
{
    public Stream Open()
    {
        if (File.Exists(path))
            return File.OpenRead(path);
        throw new FileNotFoundException("File not found.", path);
    }
    
    public static implicit operator StringFileSource(string path) => new StringFileSource(path);
}

// ReSharper disable once UnusedType.Global
public class StringFileSourceTypeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string path)
        {
            return new StringFileSource(path);
        }

        throw new NotSupportedException("Cannot convert to IFileSource.");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
namespace AvaloniaPdfViewer;

public readonly struct Zoom
{
    private readonly double _value;
    private readonly string _name;

    public static Zoom Automatic => new Zoom(0, "Automatic");

    private Zoom(double value, string name)
    {
        _value = value;
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }

    public static implicit operator Zoom(double d) => new(d, $"{d*100}%");
    public static implicit operator double(Zoom z) => z._value;
}
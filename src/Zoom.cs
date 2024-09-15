namespace AvaloniaPdfViewer;

public readonly struct Zoom : IComparable, IComparable<Zoom>, IEquatable<Zoom>
{
    private readonly double _value;
    private readonly string _name;

    public static Zoom Automatic => new Zoom(0, "Automatic");

    private Zoom(double value, string name)
    {
        _value = value;
        _name = name;
    }

    public int CompareTo(Zoom other)
    {
        return _value.CompareTo(other._value);
    }

    public override string ToString()
    {
        return _name;
    }

    public int CompareTo(object? obj)
    {
        if (obj is Zoom z)
        {
            return CompareTo(z);
        }
        return -1;
    }


    public static implicit operator Zoom(double d) => new(d, $"{d*100}%");
    public static implicit operator double(Zoom z) => z._value;

    public bool Equals(Zoom other)
    {
        return _value.Equals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Zoom other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}
namespace SquareApi.BusinessDomain;

public class Point : IEquatable<Point>, IComparable<Point>
{
    public int X { get; init; }
    public int Y { get; init; }

    public bool Equals(Point? other)
    {
        return X == other?.X
            && Y == other?.Y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string? ToString()
    {
        return $"X: {X}; Y: {Y}";
    }

    public int CompareTo(Point? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (X == other.X)
        {
            return Y.CompareTo(other.Y);
        }
        return X.CompareTo(other.X);
    }
}

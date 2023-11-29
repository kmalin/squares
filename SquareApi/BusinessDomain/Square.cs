namespace SquareApi.BusinessDomain;

public class Square : IEquatable<Square>
{
    /// <summary>
    /// 4 points of a square ordered from "smallest" to "biggest".
    /// </summary>
    public IReadOnlyList<Point> Points { get; private set; }

    public Square(IList<Point> points)
    {
        if (points.Count != 4)
        {
            throw new ArgumentException("Square should contain exactly 4 points.");
        }

        var tempList = points.ToList();
        tempList.Sort();
        var firstPoint = tempList.First();
        var firstPointIndex = points.IndexOf(firstPoint);

        var reorderedPoints = points.Skip(firstPointIndex).Concat(points.Take(firstPointIndex));
        Points = reorderedPoints.ToList().AsReadOnly();
    }

    public bool Equals(Square? other)
    {
        return Points.SequenceEqual(other?.Points ?? new List<Point>());
    }

    public override bool Equals(object? obj)
    {
        if (obj is Square other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Points[0].GetHashCode(), 
            Points[1].GetHashCode(), 
            Points[2].GetHashCode(), 
            Points[3].GetHashCode());
    }
}
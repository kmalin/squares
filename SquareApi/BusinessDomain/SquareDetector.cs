namespace SquareApi.BusinessDomain;

public class SquareDetector : ISquareDetector
{
    /// <inheritdoc/>
    public IList<Square> DetectSquares(IList<Point> points)
    {
        HashSet<Point> pointSet = points.ToHashSet();
        HashSet<Square> squareSet = new();

        // Go through all unique pairs of points.
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                Square? square = DetectSquare(points[i], points[j], pointSet);
                if (square != null)
                {
                    // We should get each square twice because square has two pairs of opposing diagonal points.
                    // Need to keep only unique square, which works using this logic because square is IEquatable
                    squareSet.Add(square);
                }
            }
        }

        return squareSet.ToList();
    }

    /// <summary>
    /// Detects a square from two points that should go into diagonal of the square.
    /// Other two points are calculated using middle point and half difference measurements.
    /// This works because of special properties of square shape.
    /// </summary>
    private static Square? DetectSquare(Point a, Point c, HashSet<Point> pointSet)
    {
        decimal diffX = (decimal)c.X - a.X;
        decimal diffY = (decimal)c.Y - a.Y;
        if (diffX == 0 && diffY == 0)
        {
            return null; // This is duplicated point, no square can be formed.
        }

        decimal bX = a.X + diffX / 2 - diffY / 2; // Mid point minus switched coordinate half difference
        decimal bY = a.Y + diffY / 2 + diffX / 2; // .. and variation

        decimal dX = a.X + diffX / 2 + diffY / 2; // Same for the third point
        decimal dY = a.Y + diffY / 2 - diffX / 2;

        // Check that calculated points are valid integers and they exist in a set of points.
        Point? b = DetectExistingPoint(bX, bY, pointSet);
        if (b == null)
        {
            return null;
        }
        Point? d = DetectExistingPoint(dX, dY, pointSet);
        if (d == null)
        {
            return null;
        }

        // Everything alright - return found square.
        return new Square(new List<Point> { a, b, c, d });
    }

    private static Point? DetectExistingPoint(decimal x, decimal y, HashSet<Point> pointSet)
    {
        if (!IsValidInteger(x) || !IsValidInteger(y)) // check that point has integer coordinates
        {
            return null;
        }
        Point b = new() { X = (int)x, Y = (int)y };
        if (!pointSet.Contains(b)) // and that it exists in a set of points
        {
            return null;
        }
        return b;
    }

    private static bool IsValidInteger(decimal value)
    {
        return (value % 1) == 0 && value >= int.MinValue && value <= int.MaxValue;
    }
}

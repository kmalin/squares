namespace SquareApi.BusinessDomain;

public interface ISquareDetector
{
    /// <summary>
    /// Finds all squares for given list of points.
    /// Finds only unique squares.
    /// </summary>
    IList<Square> DetectSquares(IList<Point> points);
}

using SquareApi.BusinessDomain;

namespace SquareApi.Tests.BusinessDomain;

public class SquareDetectorPerformanceTest
{
    [Fact]
    public void DetectSquares_Should_HandleLargeNumberOfPoints()
    {
        var sut = new SquareDetector();
        var points = new List<Point>();
        var rand = new Random();

        for (int i = 0; i < 4_000; i++)
        {
            points.Add(new Point { X = rand.Next(), Y = rand.Next() });
        }

        // Act
        sut.DetectSquares(points);

        Assert.True(true);
    }
}
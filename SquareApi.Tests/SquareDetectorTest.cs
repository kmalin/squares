using SquareApi.BusinessDomain;

namespace SquareApi.Tests;

public class SquareDetectorTest
{
    private readonly Point _a = new() { X = -1, Y = 1 };
    private readonly Point _b = new() { X = 1, Y = 1 };
    private readonly Point _c = new() { X = 1, Y = -1 };
    private readonly Point _d = new() { X = -1, Y = -1 };
    private readonly Point _e = new() { X = -1, Y = -2 };
    private readonly Point _f = new() { X = 1, Y = 3 };
    private readonly Point _g = new() { X = -1, Y = 3 };
    private readonly Point _h = new() { X = 0, Y = 0 };
    private readonly Point _i = new() { X = 0, Y = 1 };
    private readonly Point _j = new() { X = 1, Y = 0 };
    private readonly Point _k = new() { X = 0, Y = -1 };
    private readonly Point _l = new() { X = -1, Y = 0 };

    [Fact]
    public void DetectSquares_Should_HandlePointDuplicationsGracefully()
    {
        var sut = new SquareDetector();

        // Act
        var squares = sut.DetectSquares(new List<Point>() { _a, _a });

        Assert.Empty(squares);
    }

    [Fact]
    public void DetectSquares_Should_DetectOneSquare_With_MinimalScenario()
    {
        var sut = new SquareDetector();

        // Act
        var squares = sut.DetectSquares(new List<Point>() { _a, _b, _c, _d });

        Assert.Single(squares);
        var expectedSquare = new Square(new List<Point>() { _d, _a, _b, _c });
        Assert.Equal(expectedSquare, squares.Single());
    }

    [Fact]
    public void DetectSquares_Should_DetectBothSquares_When_SquaresOverlap()
    {
        var sut = new SquareDetector();

        // Act
        var squares = sut.DetectSquares(new List<Point>() { _a, _b, _c, _d, _e, _f, _g });

        Assert.Equal(2, squares.Count);
        var expectedSquare = new Square(new List<Point>() { _d, _a, _b, _c });
        Assert.Equal(expectedSquare, squares.First());
        expectedSquare = new Square(new List<Point>() { _a, _g, _f, _b });
        Assert.Equal(expectedSquare, squares.Last());
    }

    [Fact]
    public void DetectSquares_Should_DetectMultipleSquares_When_CenterPointExists()
    {
        var sut = new SquareDetector();

        // Act
        var squares = sut.DetectSquares(new List<Point>() { _a, _b, _c, _d, _h, _i, _j, _k, _l });

        Assert.Equal(6, squares.Count); // 1 big one, 4 small ones, and 1 diagonal
    }

    [Fact]
    public void DetectSquares_Should_HandleExtremeValuesWithoutCrashing()
    {
        var a = new Point { X = int.MaxValue, Y = int.MinValue };
        var b = new Point { X = int.MaxValue, Y = int.MaxValue };
        var c = new Point { X = int.MinValue, Y = int.MinValue };
        var d = new Point { X = int.MinValue, Y = int.MaxValue };
        var e = new Point { X = 0, Y = 0 };

        var sut = new SquareDetector();

        // Act - Should not form a square because there are only 3 points
        var squares = sut.DetectSquares(new List<Point>() { a, b, e });

        Assert.Empty(squares);

        // Act - Should form a square
        squares = sut.DetectSquares(new List<Point>() { a, b, c, d, e });

        Assert.Single(squares);
        var expectedSquare = new Square(new List<Point>() { c, d, b, a });
        Assert.Equal(expectedSquare, squares.Single());
    }
}
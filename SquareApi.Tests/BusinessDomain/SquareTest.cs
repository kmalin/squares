using SquareApi.BusinessDomain;

namespace SquareApi.Tests.BusinessDomain;

public class SquareTest
{
    [Fact]
    public void Constructor_Should_CheckNumberOfPoints()
    {
        var a = new Point { X = -1, Y = 1 };
        var b = new Point { X = 1, Y = 1 };
        var c = new Point { X = 1, Y = -1 };
        var d = new Point { X = -1, Y = -1 };
        var e = new Point { X = -1, Y = -2 };

        var points = new List<Point>() { a, b, c, d };

        Assert.Throws<ArgumentException>(() => { new Square(new List<Point>() { a, b, c }); });
        Assert.Throws<ArgumentException>(() => { new Square(new List<Point>() { a, b, c, d, e }); });
    }

    [Fact]
    public void Constructor_Should_OrderPoints()
    {
        var a = new Point { X = -1, Y = 1 };
        var b = new Point { X = 1, Y = 1 };
        var c = new Point { X = 1, Y = -1 };
        var d = new Point { X = -1, Y = -1 };

        var points = new List<Point>() { a, b, c, d };

        var result = new Square(points);

        Assert.Equal(d, result.Points[0]);
        Assert.Equal(a, result.Points[1]);
        Assert.Equal(b, result.Points[2]);
        Assert.Equal(c, result.Points[3]);
    }

    [Fact]
    public void Equals_Should_DetectReorderedSquareAsTheSame()
    {
        var a = new Point { X = -1, Y = 1 };
        var b = new Point { X = 1, Y = 1 };
        var c = new Point { X = 1, Y = -1 };
        var d = new Point { X = -1, Y = -1 };

        var square1 = new Square(new List<Point>() { a, b, c, d });
        var square2 = new Square(new List<Point>() { d, a, b, c });

        Assert.Equal(square1, square2);
        Assert.Equal(square1.GetHashCode(), square2.GetHashCode());
    }
}
using SquareApi.BusinessDomain;

namespace SquareApi.Tests.BusinessDomain;

public class PointTest
{
    [Fact]
    public void Equals_Should_ReturnTrueForEqualPoints()
    {
        var a = new Point { X = 13, Y = 1 };
        var b = new Point { X = 13, Y = 1 };

        Assert.True(a.Equals(b));
    }


    [Fact]
    public void Equals_Should_ReturnFalseForUnequalPoints()
    {
        var a = new Point { X = 0, Y = 1 };
        var b = new Point { X = 13, Y = 1 };
        var c = new Point { X = 13, Y = 14 };

        Assert.False(a.Equals(b));
        Assert.False(b.Equals(c));
    }

    [Fact]
    public void GetHashCode_Should_ReturnSameValueForEqualPoints()
    {
        var a = new Point { X = 13, Y = 1 };
        var b = new Point { X = 13, Y = 1 };

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }


    [Fact]
    public void GetHashCode_Should_ReturnDifferingValuesForUnequalPoints()
    {
        var a = new Point { X = 0, Y = 1 };
        var b = new Point { X = 13, Y = 1 };
        var c = new Point { X = 13, Y = 14 };

        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.NotEqual(b.GetHashCode(), c.GetHashCode());
    }

    [Fact]
    public void ToString_Should_ReturnStringValue()
    {
        var a = new Point { X = 13, Y = 1 };

        Assert.Equal("X: 13; Y: 1", a.ToString());
    }

    [Fact]
    public void CompareTo_Should_SortPointsByXThenByY()
    {
        var a = new Point { X = 0, Y = 1 };
        var b = new Point { X = 13, Y = 14 };
        var c = new Point { X = 13, Y = 1 };
        var d = new Point { X = -1, Y = 1 };

        var list = new List<Point>() { a, b, c, d };

        list.Sort();

        Assert.Equal(d, list[0]); // -1; 1
        Assert.Equal(a, list[1]); // 0; 1
        Assert.Equal(c, list[2]); // 13; 1
        Assert.Equal(b, list[3]); // 13; 14
    }
}
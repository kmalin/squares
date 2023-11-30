using SquareApi.BusinessDomain;
using SquareApi.Controllers;

namespace SquareApi.Tests.Controllers;

public class ApiMapperTest
{
    [Fact]
    public void SquareToArrays_Should_ConvertSquareToArray()
    {
        var a = new Point() { X = -1, Y = -1 };
        var b = new Point() { X = -1, Y = 1 };
        var c = new Point() { X = 1, Y = 1 };
        var d = new Point() { X = 1, Y = -1 };

        var square = new Square(new List<Point> { a, b, c, d });

        // Act
        int[][] result = ApiMapper.SquareToArrays(square);

        Assert.Equal(4, result.Length);
        CheckCoordArray(result[0], -1, -1);
        CheckCoordArray(result[1], -1, 1);
        CheckCoordArray(result[2], 1, 1);
        CheckCoordArray(result[3], 1, -1);
    }

    private static void CheckCoordArray(int[] array, int x, int y)
    {
        Assert.Equal(2, array.Length);
        Assert.Equal(x, array[0]);
        Assert.Equal(y, array[1]);
    }

    [Fact]
    public void ArrayToPoint_Should_ConvertCoordArrayToPoint()
    {
        // Act
        Point result = ApiMapper.ArrayToPoint(new int[] { -1, 1 });

        Assert.Equal(-1, result.X);
        Assert.Equal(1, result.Y);
    }

    [Fact]
    public void ArrayToPoint_Should_HandleDefaultValues()
    {
        // Act
        Point result = ApiMapper.ArrayToPoint(new int[] { -1 });

        Assert.Equal(-1, result.X);
        Assert.Equal(0, result.Y);

        // Act
        result = ApiMapper.ArrayToPoint(Array.Empty<int>());

        Assert.Equal(0, result.X);
        Assert.Equal(0, result.Y);
    }
}
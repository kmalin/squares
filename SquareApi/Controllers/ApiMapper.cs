using SquareApi.BusinessDomain;

namespace SquareApi.Controllers;

public class ApiMapper
{
    public static int[][] SquareToArrays(Square square)
    {
        var result = new int[square.Points.Count][];
        for (int i = 0; i < square.Points.Count; i++)
        {
            result[i] = new int[2];
            result[i][0] = square.Points[i].X;
            result[i][1] = square.Points[i].Y;
        }
        return result;
    }

    public static Point ArrayToPoint(int[] array)
    {
        var point = new Point { 
            X = array.Length < 1 ? 0 : array[0], // Allow skipping values in the input by assuming zero values
            Y = array.Length < 2 ? 0 : array[1] 
        };
        return point;
    }
}

using Microsoft.Extensions.Logging;
using SquareApi.BusinessDomain;
using SquareApi.Repositories;
using Xunit.Abstractions;

namespace SquareApi.Tests.BusinessDomain;

public class SquareServiceTest
{
    private readonly Point _a = new() { X = -1, Y = 1 };
    private readonly Point _b = new() { X = 1, Y = 1 };
    private readonly Point _c = new() { X = 1, Y = -1 };
    private readonly Point _d = new() { X = -1, Y = -1 };
    private readonly Point _e = new() { X = -1, Y = -2 };
    private readonly Point _f = new() { X = 1, Y = 3 };
    private readonly Point _g = new() { X = -1, Y = 3 };

    private readonly ILogger<SquareService> _logger;
    private readonly IPointRepository _pointRepo;
    private readonly ISquareRepository _squareRepo;

    public SquareServiceTest(ITestOutputHelper output)
    {
        _logger = output.BuildLoggerFor<SquareService>();
        _pointRepo = new InMemoryPointRepository();
        _squareRepo = new InMemorySquareRepository();
    }

    private SquareService CreateSut()
    {
        var result = new SquareService(_logger,
            _pointRepo,
            _squareRepo,
            new SquareDetector());
        return result;
    }

    private IList<Point> PointsForTwoSquares()
    {
        return new List<Point>() { _a, _b, _c, _d, _e, _f, _g };
    }

    [Fact]
    public async Task SetList_Should_SetPointsAndSquares()
    {
        var sut = CreateSut();

        // Act
        await sut.SetList(PointsForTwoSquares());

        Assert.Equal(7, (await _pointRepo.GetAll()).Count);
        Assert.Equal(2, (await _squareRepo.GetAll()).Count);
    }

    [Fact]
    public async Task AddPoint_Should_AddPointAndSquares()
    {
        var sut = CreateSut();

        // Act
        await sut.AddPoint(_a);
        await sut.AddPoint(_b);
        await sut.AddPoint(_c);

        Assert.Equal(3, (await _pointRepo.GetAll()).Count);
        Assert.Equal(0, (await _squareRepo.GetAll()).Count);

        // Act
        await sut.AddPoint(_d);

        Assert.Equal(4, (await _pointRepo.GetAll()).Count);
        Assert.Equal(1, (await _squareRepo.GetAll()).Count);
    }

    [Fact]
    public async Task DeletePoint_Should_RemovePointAndSquares()
    {
        var sut = CreateSut();

        // Act
        await sut.SetList(PointsForTwoSquares());

        Assert.Equal(7, (await _pointRepo.GetAll()).Count);
        Assert.Equal(2, (await _squareRepo.GetAll()).Count);

        // Act
        await sut.DeletePoint(_a);

        Assert.Equal(6, (await _pointRepo.GetAll()).Count);
        Assert.Equal(0, (await _squareRepo.GetAll()).Count);
    }

    [Fact]
    public async Task DeletePoint_Should_HandleUnknownPointsGracefully()
    {
        var sut = CreateSut();

        // Act
        await sut.SetList(new List<Point>() { _a, _b, _c, _d });

        Assert.Equal(4, (await _pointRepo.GetAll()).Count);
        Assert.Equal(1, (await _squareRepo.GetAll()).Count);

        // Act
        await sut.DeletePoint(_e);

        Assert.Equal(4, (await _pointRepo.GetAll()).Count);
        Assert.Equal(1, (await _squareRepo.GetAll()).Count);
    }

    [Fact]
    public async Task GetSquares_Should_ReturnDetectedSquares()
    {
        var sut = CreateSut();

        // Act
        await sut.SetList(PointsForTwoSquares());

        // Act
        IList<Square> squares = await sut.GetSquares();

        Assert.Equal(2, squares.Count);
        Assert.Equal(new Square(new List<Point> { _a, _b, _c, _d }), squares[0]);
        Assert.Equal(new Square(new List<Point> { _a, _g, _f, _b }), squares[1]);
    }
}
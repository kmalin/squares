using SquareApi.Controllers;
using SquareApi.Repositories;

namespace SquareApi.BusinessDomain;

public class SquareService : ISquareService
{
    private readonly ILogger<SquareService> _logger;
    private readonly IPointRepository _pointRepository;
    private readonly ISquareRepository _squareRepository;
    private readonly ISquareDetector _squareDetector;


    public SquareService(ILogger<SquareService> logger,
        IPointRepository pointRepository,
        ISquareRepository squareRepository,
        ISquareDetector squareDetector)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _pointRepository = pointRepository ?? throw new ArgumentNullException(nameof(pointRepository));
        _squareRepository = squareRepository ?? throw new ArgumentNullException(nameof(squareRepository));
        _squareDetector = squareDetector ?? throw new ArgumentNullException(nameof(squareDetector));
    }

    /// <inheritdoc/>
    public async Task SetList(IList<Point> list)
    {
        _logger.LogInformation("Importing a list of points.");
        await _pointRepository.SetList(list);
        await DetectSquares();
    }

    /// <inheritdoc/>
    public async Task AddPoint(Point point)
    {
        _logger.LogInformation("Adding a point.");
        await _pointRepository.AddPoint(point);
        await DetectSquares();
    }

    /// <inheritdoc/>
    public async Task DeletePoint(Point point)
    {
        _logger.LogInformation("Deleting a point.");
        await _pointRepository.DeletePoint(point);
        await DetectSquares();
    }

    /// <summary>
    /// Recalculates all squares and saves them into special repository.
    /// </summary>
    private async Task DetectSquares()
    {
        var points = await _pointRepository.GetAll();
        var squares = _squareDetector.DetectSquares(points);
        await _squareRepository.SetAll(squares);
    }

    /// <inheritdoc/>
    public Task<IList<Square>> GetSquares()
    {
        _logger.LogInformation("Getting a list of squares.");
        // Here we could either lock for more consistent results.
        // Or keep in un-locked for faster results.
        return _squareRepository.GetAll();
    }
}
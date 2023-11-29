using SquareApi.BusinessDomain;

namespace SquareApi.Repositories;

public interface IPointRepository
{
    /// <summary>
    /// Returns all points loaded.
    /// </summary>
    Task<IList<Point>> GetAll();

    /// <summary>
    /// Loads new set of points.
    /// Existing old list is reset.
    /// </summary>
    Task SetList(IList<Point> list);

    /// <summary>
    /// Adds one point to existing list.
    /// </summary>
    Task AddPoint(Point point);

    /// <summary>
    /// Deletes one point from existing list.
    /// If point doesn't exist, then it does nothing but doesn't fail.
    /// </summary>
    Task DeletePoint(Point point);
}

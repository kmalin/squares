using SquareApi.BusinessDomain;

namespace SquareApi.Repositories;

public interface ISquareRepository
{
    /// <summary>
    /// Stores detected squares for faster retrieval.
    /// </summary>
    Task SetAll(IList<Square> list);

    /// <summary>
    /// Returns all detected squares.
    /// </summary>
    Task<IList<Square>> GetAll();
}

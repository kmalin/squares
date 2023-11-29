using SquareApi.BusinessDomain;

namespace SquareApi.Repositories;

public class InMemorySquareRepository : ISquareRepository
{
    private readonly List<Square> _list = new();

    public Task<IList<Square>> GetAll()
    {
        return Task.FromResult((IList<Square>)_list.ToList());
    }

    public Task SetAll(IList<Square> list)
    {
        _list.Clear();
        _list.AddRange(list);
        return Task.CompletedTask;
    }
}
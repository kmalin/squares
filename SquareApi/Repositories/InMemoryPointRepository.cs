using SquareApi.BusinessDomain;

namespace SquareApi.Repositories;

public class InMemoryPointRepository : IPointRepository
{
    private readonly List<Point> _list = new();

    public Task<IList<Point>> GetAll()
    {
        return Task.FromResult((IList<Point>)_list.ToList());
    }

    public Task SetList(IList<Point> list)
    {
        _list.Clear();
        _list.AddRange(list);
        return Task.CompletedTask;
    }

    public Task AddPoint(Point point)
    {
        _list.Add(point);
        return Task.CompletedTask;
    }

    public Task DeletePoint(Point point)
    {
        _list.Remove(point);
        return Task.CompletedTask;
    }
}

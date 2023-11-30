using ServiceStack.Redis.Generic;
using SquareApi.BusinessDomain;
using StackExchange.Redis;

namespace SquareApi.Repositories.Redis;

public class RedisPointRepository : IPointRepository
{
    private readonly IRedisClientProvider _clientProvider;
    private readonly RedisKey _key = new("points");

    public RedisPointRepository(IRedisClientProvider clientProvider)
    {
        _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
    }

    public Task<IList<Point>> GetAll()
    {
        using var redisClient = _clientProvider.GetClient();
        IRedisTypedClient<Point> redisPoints = redisClient.As<Point>();
        return Task.FromResult((IList<Point>)redisPoints.Lists[_key].GetAll());
    }

    public Task SetList(IList<Point> list)
    {
        using var redisClient = _clientProvider.GetClient();
        IRedisTypedClient<Point> redisPoints = redisClient.As<Point>();

        redisPoints.Lists[_key].RemoveAll();
        redisPoints.Lists[_key].AddRange(list);

        return Task.CompletedTask;
    }

    public Task AddPoint(Point point)
    {
        using var redisClient = _clientProvider.GetClient();
        IRedisTypedClient<Point> redisPoints = redisClient.As<Point>();

        redisPoints.Lists[_key].Add(point);

        return Task.CompletedTask;
    }

    public Task DeletePoint(Point point)
    {
        using var redisClient = _clientProvider.GetClient();
        IRedisTypedClient<Point> redisPoints = redisClient.As<Point>();

        redisPoints.Lists[_key].Remove(point);

        return Task.CompletedTask;
    }
}

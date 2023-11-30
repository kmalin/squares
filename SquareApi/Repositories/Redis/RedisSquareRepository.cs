using ServiceStack.Redis.Generic;
using SquareApi.BusinessDomain;
using StackExchange.Redis;

namespace SquareApi.Repositories.Redis;

public class RedisSquareRepository : ISquareRepository
{
    private readonly IRedisClientProvider _clientProvider;
    private readonly RedisKey _key = new("squares");

    public RedisSquareRepository(IRedisClientProvider clientProvider)
    {
        _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
    }

    public Task<IList<Square>> GetAll()
    {
        using var redisClient = _clientProvider.GetClient();
        IRedisTypedClient<Square> redisSquares = redisClient.As<Square>();
        return Task.FromResult((IList<Square>)redisSquares.Lists[_key].GetAll());
    }

    public Task SetAll(IList<Square> list)
    {
        using var redisClient = _clientProvider.GetClient();
        IRedisTypedClient<Square> redisSquares = redisClient.As<Square>();

        redisSquares.Lists[_key].RemoveAll();
        redisSquares.Lists[_key].AddRange(list);

        return Task.CompletedTask;
    }
}
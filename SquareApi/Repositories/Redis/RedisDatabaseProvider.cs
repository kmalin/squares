using ServiceStack.Redis;
using StackExchange.Redis;

namespace SquareApi.Repositories.Redis;

public class RedisClientProvider : IRedisClientProvider
{
    private readonly string _host;
    private readonly int _port;

    public RedisClientProvider(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public RedisClient GetClient()
    {
        return new RedisClient(_host, _port);
    }
}

using ServiceStack.Redis;

namespace SquareApi.Repositories.Redis;

public interface IRedisClientProvider
{
    RedisClient GetClient();
}

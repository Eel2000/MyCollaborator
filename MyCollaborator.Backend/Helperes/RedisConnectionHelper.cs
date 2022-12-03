using StackExchange.Redis;

namespace MyCollaborator.Backend.Helperes;

public class RedisConnectionHelper
{
    private static Lazy<ConnectionMultiplexer> _lazyConnection;

    static RedisConnectionHelper()
    {
        RedisConnectionHelper._lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("127.0.0.1:6379");
        });
    }

    public static ConnectionMultiplexer Connection => _lazyConnection.Value;
}
using MyCollaborator.Backend.Helperes;
using MyCollaborator.Backend.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MyCollaborator.Backend.Services;

public class CachingService : ICachingService
{
    private readonly IDatabase _database;

    public CachingService()
    {
        _database = RedisConnectionHelper.Connection.GetDatabase();
    }

    public async ValueTask RemoveItemFromCacheAsync(string key)
    {
        if (_database.KeyExists(key))
        {
            await _database.KeyDeleteAsync(key);
        }
    }

    public async ValueTask<T> GetItemFromCacheAsync<T>(string key)
    {
        if (await _database.KeyExistsAsync(key))
        {
            var rawData = _database.StringGet(key);
            if (!string.IsNullOrWhiteSpace(rawData))
            {
                return JsonConvert.DeserializeObject<T>(rawData);
            }
        }

        return default;
    }

    public async ValueTask<bool> SaveItemInTheCacheAsync<T>(string key, T item, DateTimeOffset limitTime)
    {
        TimeSpan expirationTime = limitTime.DateTime.Subtract(DateTime.Now);
        return await _database.StringSetAsync(key, JsonConvert.SerializeObject(value: item), expirationTime);
    }
}
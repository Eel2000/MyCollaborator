using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Services.Interfaces;

public interface ICachingService
{
    ValueTask RemoveItemFromCacheAsync(string key);
    ValueTask<T> GetItemFromCacheAsync<T>(string key);
    ValueTask<IEnumerable<string>> GetAllCachedDataAsync();
    ValueTask<bool> SaveItemInTheCacheAsync<T>(string key, T item, DateTimeOffset limitTime);
}
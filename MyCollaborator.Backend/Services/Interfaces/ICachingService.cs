namespace MyCollaborator.Backend.Services.Interfaces;

public interface ICachingService
{
    ValueTask RemoveItemFromCacheAsync(string key);
    ValueTask<T> GetItemFromCacheAsync<T>(string key);
    ValueTask<bool> SaveItemInTheCacheAsync<T>(string key, T item, DateTimeOffset limitTime);
}
using System.Text.Json;
using Microsoft.JSInterop;

namespace MyCollaborator.Client.UI.Interops;

public sealed class LocalStorageInterop
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Remove everything from the nivigator local-storage
    /// </summary>
    public ValueTask Clear() => _jsRuntime.InvokeVoidAsync("localStorage.clear");

    /// <summary>
    /// Return the specified key's item
    /// </summary>
    /// <param name="key">the key selector of the item in the local-storage(navigator)</param>
    /// <typeparam name="T">the type of the item to get</typeparam>
    /// <returns><see cref="ValueTask{TResult}"/> where TResult is the type of item to get.</returns>
    public async ValueTask<T?> GetItemAsync<T>(string key)
    {
        var data = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        if (string.IsNullOrWhiteSpace(data))
            return default;
        return JsonSerializer.Deserialize<T>(data);
    }

    /// <summary>
    /// Save an item in the local-storage
    /// </summary>
    /// <param name="key">The key selector of the item to save(required)</param>
    /// <param name="data">the item value data</param>
    /// <typeparam name="T">The item type</typeparam>
    public async ValueTask SetItemAsync<T>(string key, T? data)
    {
        var serializedData = JsonSerializer.Serialize(data);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, serializedData);
    }
}
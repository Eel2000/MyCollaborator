using System.Net.Http.Json;
using System.Text.Json;
using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Client.UI.Service;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async ValueTask<Response<AuthenticateResponse>> AuthenticateAsync(AuthenticationQuery query)
    {
        var response = await _httpClient.PostAsJsonAsync("api/myCollaborator/Authentication/Authenticate", query);
        if (response.IsSuccessStatusCode)
        {
            var rawData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Response<AuthenticateResponse>>(rawData);
            return data;
        }

        var errorResponse = JsonSerializer.Deserialize<Response<Exception>>(await response.Content.ReadAsStringAsync());
        return new Response<AuthenticateResponse>(errorResponse.Status, errorResponse.Message);
    }

    public async ValueTask<Response<IReadOnlyList<Friends>>> GetFriendsListAsync(Guid userId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<Response<IReadOnlyList<Friends>>>(
                "api/myCollaborator/Chatting/get-friend-list?user=" + userId);
        return response;
    }

    public async ValueTask<Response<string>> SaveNewConnectionAsync(ConnectionId connectionId)
    {
        var response = await _httpClient.PostAsJsonAsync("api/myCollaborator/Chatting/save-new-connection", connectionId);
        if (response.IsSuccessStatusCode)
        {
            var data = JsonSerializer.Deserialize<Response<string>>(await response.Content.ReadAsStringAsync());
            return data;
        }

        var errorData = JsonSerializer.Deserialize<Response<Exception>>(await response.Content.ReadAsStringAsync());
        return new Response<string>(errorData.Status, errorData.Message);
    }
}
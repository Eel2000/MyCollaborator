using System.Diagnostics;
using System.Net.Http.Json;
using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyCollaborator.Client.UI.Service;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<User>> AuthenticateAsync(AuthenticationQuery query)
    {
        try
        {
            var jData = JsonConvert.SerializeObject(query);
            var body = new StringContent(jData, System.Text.Encoding.UTF8, "application/rawData");
            var response = await _httpClient.PostAsJsonAsync("/api/myCollaborator/Authentication/Authenticate", query);
            if (response.IsSuccessStatusCode)
            {
                var rawData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Response<User>>(rawData);

                return data;
            }

            var errorResponse = JsonConvert.DeserializeObject<Response<Exception>>(await response.Content.ReadAsStringAsync());
            return new Response<User>(errorResponse.Status, errorResponse.Message);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return new Response<User>(Status.ERROR, e.Message);
        }
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
            var data = JsonConvert.DeserializeObject<Response<string>>(await response.Content.ReadAsStringAsync());
            return data;
        }

        var errorData = JsonConvert.DeserializeObject<Response<Exception>>(await response.Content.ReadAsStringAsync());
        return new Response<string>(errorData.Status, errorData.Message);
    }
}
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MyCollaborator.Client.UI.Interops;
using MyCollaborator.Client.UI.Service;
using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Client.UI.Pages.Logins;

public partial class Login : ComponentBase
{
    [Inject] public ApiService Api { get; set; }
    [Inject] public NavigationManager Navigator { get; set; }
    [Inject] public LocalStorageInterop LocalStorageInterop { get; set; }

    private AuthenticationQuery _query = new();
    private HubConnection hubConnection;

    private string _username;
    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            _query.Username = value;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7225").Build();
        await hubConnection.StartAsync();

        var user = await LocalStorageInterop.GetItemAsync<User>("connectedUser");
        if (user is not null)
        {
            await SaveConnectionAsync(user);
            Navigator.NavigateTo("/index");
        }

        hubConnection.On<Response<string>>("SaveConnection", res =>
        {
            var dataReturned = res;
            Console.WriteLine(res.Message);
        });

    }

    async Task LogIng()
    {
        if (!string.IsNullOrWhiteSpace(_query.Telephone) || !string.IsNullOrWhiteSpace(_query.Username))
        {
            var apiCall = await Api.AuthenticateAsync(_query).ConfigureAwait(true);
            if (apiCall.Status == Status.SUCCESS)
            {
                await LocalStorageInterop.SetItemAsync("connectedUser", apiCall.Data);
                Navigator.NavigateTo("/index");
                return;
            }
        }
    }

    void OnTyping(ChangeEventArgs eventArgs)
    {
        var typed = eventArgs?.Value.ToString();
    }

    async Task SaveConnectionAsync(User user)
    {
        await hubConnection.StartAsync();
        await hubConnection.InvokeAsync("SaveConnectedUserConnectionIdAsync", user.Id);
    }
}

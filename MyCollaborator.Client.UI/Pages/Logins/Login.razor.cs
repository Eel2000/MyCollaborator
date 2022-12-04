using Microsoft.AspNetCore.Components;
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
        var user = await LocalStorageInterop.GetItemAsync<User>("connectedUser");
        if(user is not null)
        {
            _navigationManager.NavigateTo("/index");
        }
    }

    async Task LogIng()
    {
        if (!string.IsNullOrWhiteSpace(_query.Telephone) || !string.IsNullOrWhiteSpace(_query.Username))
        {
            var apiCall = await Api.AuthenticateAsync(_query).ConfigureAwait(true);
            if (apiCall.Status == Status.SUCCESS)
            {
                await LocalStorageInterop.SetItemAsync("connectedUser", apiCall.Data);
                _navigationManager.NavigateTo("/index");
                return;
            }
        }
    }

    void OnTyping(ChangeEventArgs eventArgs)
    {
        var typed = eventArgs?.Value.ToString();
    }
}

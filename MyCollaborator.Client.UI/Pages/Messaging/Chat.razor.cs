using Microsoft.AspNetCore.Components;
using MyCollaborator.Client.UI.Interops;
using MyCollaborator.Client.UI.Service;
using MyCollaborator.Shared.Models;
using System.Diagnostics;
using MyCollaborator.Shared.DTOs;
using System.Collections.ObjectModel;

namespace MyCollaborator.Client.UI.Pages.Messaging;

public partial class Chat : ComponentBase
{
    [Inject] public LocalStorageInterop LocalStorage { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public ApiService Api { get; set; }

    //TODO: Implement the logic for loading the friend list and chats onclicks
    private User user = default!;
    private ObservableCollection<Friends> _friends;
    private Friends selectedFriend = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _friends = new();
            var usr = await LocalStorage.GetItemAsync<User>("connectedUser");
            if (usr is not null)
            {
                user = usr;
                var apiCall = await Api.GetFriendsListAsync(user.Id);
                if(apiCall.Status == Status.SUCCESS)
                {
                    var friendsList = apiCall.Data;
                    _friends = new(friendsList);
                }
            }
            else Navigation.NavigateTo("/");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Navigation.NavigateTo("/");
        }
    }

    void OnFriendSelected(Friends friend) => selectedFriend = friend;//TODO: load the friend message if any.
}

using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Services.Interfaces;

public interface IChattingService
{
    ValueTask<Response<IReadOnlyList<Friends>>> GetFriendsAsync(Guid user);
    ValueTask<Response<string>> SaveUserConnectionId(ConnectionId connectionId);
}
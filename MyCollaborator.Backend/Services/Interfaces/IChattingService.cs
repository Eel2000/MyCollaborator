using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Services.Interfaces;

public interface IChattingService
{
    ValueTask<Shared.DTOs.Response<IReadOnlyList<Friends>>> GetFriendsAsync(Guid user);
}
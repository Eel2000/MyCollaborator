using MyCollaborator.Shared.DTOs;
using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Hubs.Interfaces;

public interface IChattingHub
{
    ValueTask ReceiveFromAll(Message message);
    ValueTask ReceiveFromFriend(Message message);
    ValueTask UserTyping(bool isTyping);
    ValueTask UsernameChecking(Response<string> response);
    ValueTask ReceiveConnectionId(string connectionId);
}
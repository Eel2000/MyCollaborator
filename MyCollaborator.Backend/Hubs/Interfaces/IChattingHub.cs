using MyCollaborator.Shared.Models;

namespace MyCollaborator.Backend.Hubs.Interfaces;

public interface IChattingHub
{
    ValueTask ReceiveFromAll(Message message);
    ValueTask ReceiveFromFriend(Message message);
}
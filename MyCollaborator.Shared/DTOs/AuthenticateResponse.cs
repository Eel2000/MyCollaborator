using MyCollaborator.Shared.Models;

namespace MyCollaborator.Shared.DTOs;

public class AuthenticateResponse
{
    public User User { get; set; }
    public ICollection<Message> Messages { get; set; }
    public  ICollection<Friends> FriendsCollection { get; set; }
}
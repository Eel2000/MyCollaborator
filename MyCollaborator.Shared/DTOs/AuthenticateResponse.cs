using MyCollaborator.Shared.Models;

namespace MyCollaborator.Shared.DTOs;

public class AuthenticateResponse
{
    public User? User { get; set; }
    public List<Message> Messages { get; set; }
    public  List<Friends> FriendsCollection { get; set; }
}
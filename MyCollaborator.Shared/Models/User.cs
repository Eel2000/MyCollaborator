namespace MyCollaborator.Shared.Models;

public class User
{
    public User()
    {
        UserConnections = new HashSet<UserConnection>();
    }
    
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Telephone { get; set; }
    
    public virtual ICollection<UserConnection> UserConnections { get; set; }
}
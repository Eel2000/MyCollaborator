namespace MyCollaborator.Shared.Models;

public class User
{
    public User()
    {
        Messages = new HashSet<Message>();
    }
    
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Telephone { get; set; }
    
    public virtual ICollection<Message> Messages { get; set; }
}
namespace MyCollaborator.Shared.Models;

public class UserConnection
{
    public Guid Id { get; set; }
    public string Connection { get; set; }
    public DateTimeOffset LastConnection { get; set; }
    public Guid UserId { get; set; }
    
    public  virtual User User { get; set; }
}
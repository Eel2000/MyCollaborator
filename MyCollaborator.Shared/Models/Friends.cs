namespace MyCollaborator.Shared.Models;

public class Friends
{
    public Guid Id { get; set; }
    public Guid UserRequestee { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    
    public virtual User User { get; set; }
}
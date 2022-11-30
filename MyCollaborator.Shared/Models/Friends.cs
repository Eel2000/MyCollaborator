namespace MyCollaborator.Shared.Models;

public class Friends
{
    public Guid Id { get; set; }
    public Guid UserRequestee { get; set; }
    public Guid UserRequester { get; set; }
    public bool IsActive { get; set; }
}
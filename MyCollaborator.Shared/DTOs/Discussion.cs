using MyCollaborator.Shared.Models;

namespace MyCollaborator.Shared.DTOs;

public class Discussion
{
    public Guid Id { get; set; }
    public Guid From { get; set; }
    public Guid To { get; set; }
    public  string Content { get; set; }
    public  DateTimeOffset DateTime { get; set; }
    public User Sender { get; set; }
}
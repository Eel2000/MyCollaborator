namespace MyCollaborator.Shared.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid From { get; set; }
    public Guid To { get; set; }
    public  string Content { get; set; }
    public  DateTimeOffset DateTime { get; set; }
}
namespace MyCollaborator.Client.UI.Models.Navigations;

public record MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public string Href { get; set; }
}
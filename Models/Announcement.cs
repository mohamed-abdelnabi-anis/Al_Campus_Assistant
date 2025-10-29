namespace Al_Campus_Assistant.Models;

public class Announcement
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public bool IsImportant { get; set; }
    public string Category { get; set; }
}

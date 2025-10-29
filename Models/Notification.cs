namespace Al_Campus_Assistant.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Type { get; set; } // "info", "warning", "success", "emergency"
    public int UserId { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ExpiresAt { get; set; } // تاريخ انتهاء الإشعار
}

public class NotificationRequest
{
    public string Title { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
    public int UserId { get; set; }
    public DateTime? ExpiresAt { get; set; }
}
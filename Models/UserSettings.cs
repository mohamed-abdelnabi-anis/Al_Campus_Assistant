using System.ComponentModel.DataAnnotations;

namespace Al_Campus_Assistant.Models;

public class UserSettings
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    // General
    [StringLength(20)]
    public string Language { get; set; } = "العربية"; // "العربية" أو "English"

    public bool DarkMode { get; set; } = false;

    // Notifications
    public bool LectureAlerts { get; set; } = true;
    public bool ExamAlerts { get; set; } = true;
    public bool AnnouncementAlerts { get; set; } = true;
    public bool EmailNotifications { get; set; } = false;
    public bool PushNotifications { get; set; } = true;

    // Additional settings
    [StringLength(10)]
    public string FontSize { get; set; } = "medium"; // small, medium, large

    public bool Vibration { get; set; } = true;
    public bool Sound { get; set; } = true;

    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public virtual User? User { get; set; }
}

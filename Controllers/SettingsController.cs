
using Microsoft.AspNetCore.Mvc;
using Al_Campus_Assistant.Data;
using Al_Campus_Assistant.Models;
using Microsoft.EntityFrameworkCore;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SettingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Settings/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetSettings(int userId)
    {
        try
        {
            // هنا لازم يكون عندك Settings model أو تخزن في User
            // حالياً هنرجع إعدادات افتراضية
            return Ok(new
            {
                success = true,
                data = new
                {
                    general = new
                    {
                        language = "العربية", // أو "English"
                        darkMode = true,      // أو false
                        fontSize = "medium",  // small, medium, large
                        notificationsEnabled = true
                    },
                    notifications = new
                    {
                        lectureAlerts = true,
                        examAlerts = true,
                        announcementAlerts = true,
                        emailNotifications = false,
                        pushNotifications = true
                    },
                    privacy = new
                    {
                        showProfile = true,
                        showEmail = false,
                        showPhone = false
                    },
                    account = new
                    {
                        userId,
                        lastUpdated = DateTime.Now
                    }
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في جلب الإعدادات",
                error = ex.Message
            });
        }
    }

    // PUT: api/Settings/{userId}
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateSettings(int userId, [FromBody] UpdateSettingsRequest request)
    {
        try
        {
            // التحقق من وجود المستخدم
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            // هنا في الواقع تخزن الإعدادات في قاعدة البيانات
            // لكن حالياً نرجع نجاح فقط

            return Ok(new
            {
                success = true,
                message = "تم تحديث الإعدادات بنجاح",
                updatedAt = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في تحديث الإعدادات",
                error = ex.Message
            });
        }
    }

    // POST: api/Settings/{userId}/logout
    [HttpPost("{userId}/logout")]
    public async Task<IActionResult> Logout(int userId)
    {
        try
        {
            // في حالة JWT: تجعل التوكن غير صالح
            // في حالتنا: نرجع نجاح فقط

            return Ok(new
            {
                success = true,
                message = "تم تسجيل الخروج بنجاح",
                timestamp = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في تسجيل الخروج",
                error = ex.Message
            });
        }
    }

    // GET: api/Settings/{userId}/notifications
    [HttpGet("{userId}/notifications")]
    public async Task<IActionResult> GetNotificationSettings(int userId)
    {
        // إعدادات الإشعارات فقط
        return Ok(new
        {
            success = true,
            data = new
            {
                lectureAlerts = true,
                examAlerts = true,
                announcementAlerts = true,
                emailNotifications = false,
                pushNotifications = true,
                vibration = true,
                sound = true
            }
        });
    }
}

// نموذج تحديث الإعدادات
public class UpdateSettingsRequest
{
    public string? Language { get; set; }
    public bool? DarkMode { get; set; }
    public bool? LectureAlerts { get; set; }
    public bool? ExamAlerts { get; set; }
    public bool? AnnouncementAlerts { get; set; }
    public bool? EmailNotifications { get; set; }
    public bool? PushNotifications { get; set; }
    public string? FontSize { get; set; }
}
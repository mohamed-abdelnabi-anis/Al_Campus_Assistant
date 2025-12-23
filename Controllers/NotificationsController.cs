using Al_Campus_Assistant.Data;
using Al_Campus_Assistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotificationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Notifications/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserNotifications(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            // استخدم الخصائص الصحيحة من الـ Model
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new
                {
                    id = n.Id,
                    title = n.Title,
                    message = n.Message, // 👈 هنا Message مش Content
                    type = n.Type,
                    isRead = n.IsRead,
                    timeAgo = GetTimeAgo(n.CreatedAt),
                    createdAt = n.CreatedAt,
                    expiresAt = n.ExpiresAt,
                    isExpired = n.ExpiresAt.HasValue && n.ExpiresAt <= DateTime.Now
                })
                .ToListAsync();

            var unreadCount = notifications.Count(n => !n.isRead);
            var urgentCount = notifications.Count(n => n.type == "emergency");

            return Ok(new
            {
                success = true,
                message = "تم جلب الإشعارات بنجاح",
                data = new
                {
                    notifications,
                    summary = new
                    {
                        total = notifications.Count,
                        unread = unreadCount,
                        urgent = urgentCount,
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
                message = "حدث خطأ في جلب الإشعارات",
                error = ex.Message
            });
        }
    }

    // GET: api/Notifications/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotification(int id)
    {
        try
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound(new { success = false, message = "الإشعار غير موجود" });

            return Ok(new
            {
                success = true,
                data = new
                {
                    id = notification.Id,
                    title = notification.Title,
                    message = notification.Message,
                    type = notification.Type,
                    userId = notification.UserId,
                    isRead = notification.IsRead,
                    createdAt = notification.CreatedAt,
                    expiresAt = notification.ExpiresAt,
                    timeAgo = GetTimeAgo(notification.CreatedAt)
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في جلب الإشعار",
                error = ex.Message
            });
        }
    }

    // POST: api/Notifications/mark-read/{id}
    [HttpPost("mark-read/{id}")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        try
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound(new { success = false, message = "الإشعار غير موجود" });

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                success = true,
                message = "تم تحديد الإشعار كمقروء"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في تحديث الإشعار",
                error = ex.Message
            });
        }
    }

    // POST: api/Notifications/mark-all-read/{userId}
    [HttpPost("mark-all-read/{userId}")]
    public async Task<IActionResult> MarkAllAsRead(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            var unreadNotifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = $"تم تحديد {unreadNotifications.Count} إشعار كمقروء"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في تحديث الإشعارات",
                error = ex.Message
            });
        }
    }

    // DELETE: api/Notifications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        try
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound(new { success = false, message = "الإشعار غير موجود" });

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "تم حذف الإشعار بنجاح"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في حذف الإشعار",
                error = ex.Message
            });
        }
    }

    // POST: api/Notifications/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationRequest request)
    {
        try
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            var notification = new Notification
            {
                Title = request.Title,
                Message = request.Message, // 👈 هنا Message
                Type = request.Type,
                UserId = request.UserId,
                IsRead = false,
                CreatedAt = DateTime.Now,
                ExpiresAt = request.ExpiresAt
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "تم إنشاء الإشعار بنجاح",
                notificationId = notification.Id
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في إنشاء الإشعار",
                error = ex.Message
            });
        }
    }

    // GET: api/Notifications/unread-count/{userId}
    [HttpGet("unread-count/{userId}")]
    public async Task<IActionResult> GetUnreadCount(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            var count = await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead &&
                               (n.ExpiresAt == null || n.ExpiresAt > DateTime.Now));

            return Ok(new
            {
                success = true,
                count,
                hasUnread = count > 0
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في جلب عدد الإشعارات",
                error = ex.Message
            });
        }
    }

    // دالة مساعدة: حساب الوقت المنقضي
    private string GetTimeAgo(DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;

        if (timeSpan.TotalMinutes < 1)
            return "الآن";
        if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} min ago";
        if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} hours ago";
        if (timeSpan.TotalDays < 7)
            return $"{(int)timeSpan.TotalDays} days ago";

        return dateTime.ToString("yyyy-MM-dd");
    }
}

// استخدم NotificationRequest الموجود أصلاً في الـ Model
// مفيش داعي لـ CreateNotificationRequest جديد
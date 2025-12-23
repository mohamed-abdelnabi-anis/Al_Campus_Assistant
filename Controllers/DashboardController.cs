using Microsoft.AspNetCore.Mvc;
using Al_Campus_Assistant.Data;
using Al_Campus_Assistant.Models;
using Microsoft.EntityFrameworkCore;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Dashboard/stats - إحصائيات النظام (كودك الأصلي)
    [HttpGet("stats")]
    public async Task<IActionResult> GetDashboardStats()
    {
        try
        {
            // استخدم فقط الخصائص الموجودة في Models
            var totalStudents = await _context.Users.CountAsync(u => u.Role == "Student");
            var totalProfessors = await _context.Users.CountAsync(u => u.Role == "Professor");
            var totalCourses = await _context.Courses.CountAsync();
            var totalExams = await _context.Exams.CountAsync();
            var totalAnnouncements = await _context.Announcements.CountAsync();
            var activeUsers = await _context.Users.CountAsync(u => u.IsActive);

            // استخدم فقط الخصائص الموجودة في Announcement
            var recentAnnouncements = await _context.Announcements
                .Where(a => a.Date >= DateTime.Now.AddDays(-7))
                .OrderByDescending(a => a.Date)
                .Take(5)
                .Select(a => new
                {
                    a.Id,
                    a.Title,
                    a.Content,
                    a.Date,
                    a.IsImportant,
                    Category = a.Category ?? "عام" // استخدم القيمة الموجودة
                })
                .ToListAsync();

            // استخدم فقط الخصائص الموجودة في Exam
            var upcomingExams = await _context.Exams
                .Include(e => e.Course)
                .Where(e => e.ExamDate >= DateTime.Now && e.ExamDate <= DateTime.Now.AddDays(30))
                .OrderBy(e => e.ExamDate)
                .Take(5)
                .Select(e => new
                {
                    e.Id,
                    CourseName = e.Course != null ? e.Course.Name : "غير محدد",
                    Type = e.ExamType ?? "امتحان",
                    Date = e.ExamDate,
                    Time = e.Time ?? "09:00",
                    Location = e.Location ?? "قاعة المحاضرات"
                })
                .ToListAsync();

            return Ok(new
            {
                users = new
                {
                    totalStudents,
                    totalProfessors,
                    activeUsers
                },
                content = new
                {
                    totalCourses,
                    totalExams,
                    totalAnnouncements
                },
                recentAnnouncements,
                upcomingExams,
                lastUpdated = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "حدث خطأ في جلب الإحصائيات", error = ex.Message });
        }
    }

    // GET: api/Dashboard/activity - نشاط النظام (كودك الأصلي)
    [HttpGet("activity")]
    public async Task<IActionResult> GetSystemActivity()
    {
        // استخدم فقط الخصائص الموجودة في User
        var recentUsers = await _context.Users
            .OrderByDescending(u => u.CreatedAt)
            .Take(10)
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Role,
                u.CreatedAt,
                u.IsActive
            })
            .ToListAsync();

        // استخدم فقط الخصائص الموجودة
        var todayAnnouncements = await _context.Announcements
            .Where(a => a.Date.Date == DateTime.Today)
            .CountAsync();

        var thisWeekExams = await _context.Exams
            .Where(e => e.ExamDate >= DateTime.Now && e.ExamDate <= DateTime.Now.AddDays(7))
            .CountAsync();

        return Ok(new
        {
            recentUsers,
            todayAnnouncements,
            thisWeekExams,
            systemStatus = "نشط"
        });
    }

    // GET: api/Dashboard/student/{userId} - مبسط بدون خصائص إضافية
    [HttpGet("student/{userId}")]
    public async Task<IActionResult> GetStudentDashboard(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            // بيانات افتراضية بسيطة
            var courses = new[]
            {
                new { id = 1, name = "Artificial Intelligence", professor = "Dr. Sara Ahmed", progress = 75 },
                new { id = 2, name = "Mobile Development", professor = "Dr. Omar K.", progress = 40 },
                new { id = 3, name = "Advanced Databases", professor = "Dr. Youssef Ali", progress = 90 }
            };

            var exams = new[]
            {
                new { id = 1, course = "Advanced Math", date = "15", time = "9:00 AM", location = "Hall 101" },
                new { id = 2, course = "Computer Networks", date = "18", time = "1:00 PM", location = "Lab 3" }
            };

            return Ok(new
            {
                success = true,
                message = "تم جلب البيانات بنجاح",
                data = new
                {
                    student = new
                    {
                        id = user.Id,
                        name = user.Name,
                        email = user.Email,
                        phone = user.PhoneNumber
                    },
                    courses,
                    exams,
                    stats = new
                    {
                        totalCourses = 5,
                        upcomingExams = 3,
                        averageGrade = 85.5
                    }
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في جلب البيانات",
                error = ex.Message
            });
        }
    }
}
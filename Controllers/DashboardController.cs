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

    // GET: api/Dashboard/stats - إحصائيات النظام
    [HttpGet("stats")]
    public async Task<IActionResult> GetDashboardStats()
    {
        try
        {
            var totalStudents = await _context.Users.CountAsync(u => u.Role == "Student");
            var totalProfessors = await _context.Users.CountAsync(u => u.Role == "Professor");
            var totalCourses = await _context.Courses.CountAsync();
            var totalExams = await _context.Exams.CountAsync();
            var totalAnnouncements = await _context.Announcements.CountAsync();
            var activeUsers = await _context.Users.CountAsync(u => u.IsActive);

            var recentAnnouncements = await _context.Announcements
                .Where(a => a.Date >= DateTime.Now.AddDays(-7)) // الإعلانات خلال أسبوع
                .OrderByDescending(a => a.Date)
                .Take(5)
                .Select(a => new
                {
                    a.Id,
                    a.Title,
                    a.Content,
                    a.Date,
                    a.IsImportant,
                    a.Category
                })
                .ToListAsync();

            var upcomingExams = await _context.Exams
                .Where(e => e.ExamDate >= DateTime.Now && e.ExamDate <= DateTime.Now.AddDays(30))
                .OrderBy(e => e.ExamDate)
                .Take(5)
                .Select(e => new
                {
                    e.Id,
                    CourseName = e.Course.Name,
                    e.ExamType,
                    e.ExamDate,
                    e.Time,
                    e.Location
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

    // GET: api/Dashboard/activity - نشاط النظام
    [HttpGet("activity")]
    public async Task<IActionResult> GetSystemActivity()
    {
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
}
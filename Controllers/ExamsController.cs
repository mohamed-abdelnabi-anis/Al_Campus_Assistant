using Microsoft.AspNetCore.Mvc;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetExams()
    {
        var exams = new[]
        {
            new {
                Id = 1,
                Course = "برمجة التطبيقات",
                Date = "2024-12-15",
                Time = "10:00 AM",
                Location = "قاعة 101",
                Type = "منتصف الفصل",
                Duration = "3 ساعات"
            },
            new {
                Id = 2,
                Course = "قواعد بيانات",
                Date = "2024-12-17",
                Time = "01:00 PM",
                Location = "قاعة 202",
                Type = "منتصف الفصل",
                Duration = "2 ساعة"
            },
            new {
                Id = 3,
                Course = "هندسة البرمجيات",
                Date = "2025-01-10",
                Time = "09:00 AM",
                Location = "قاعة 305",
                Type = "امتحان نهائي",
                Duration = "3 ساعات"
            },
            new {
                Id = 4,
                Course = "شبكات الحاسب",
                Date = "2025-01-12",
                Time = "11:00 AM",
                Location = "قاعة 201",
                Type = "امتحان نهائي",
                Duration = "2 ساعة"
            }
        };

        return Ok(exams);
    }

    [HttpGet("upcoming")]
    public IActionResult GetUpcomingExams()
    {
        var upcomingExams = new[]
        {
            new {
                Id = 1,
                Course = "برمجة التطبيقات",
                Date = "2024-12-15",
                DaysLeft = 30,
                Important = true
            },
            new {
                Id = 2,
                Course = "قواعد بيانات",
                Date = "2024-12-17",
                DaysLeft = 32,
                Important = true
            }
        };

        return Ok(upcomingExams);
    }
}
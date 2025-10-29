using Microsoft.AspNetCore.Mvc;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAnnouncements()
    {
        var announcements = new[]
        {
            new {
                Id = 1,
                Title = "تغيير موعد محاضرة",
                Content = "تم تأجيل محاضرة قواعد بيانات إلى يوم الأربعاء الساعة 10 صباحاً",
                Date = "2024-01-16",
                Important = true,
                Category = "مهم"
            },
            new {
                Id = 2,
                Title = "نتائج الامتحانات",
                Content = "ظهرت نتائج امتحانات منتصف الفصل، يمكن الاطلاع عليها من خلال النظام",
                Date = "2024-01-15",
                Important = true,
                Category = "مهم"
            },
            new {
                Id = 3,
                Title = "اجتماع الطلاب",
                Content = "سيتم عقد اجتماع لطلاب الفرقة الثالثة يوم الخميس القادم في قاعة المؤتمرات",
                Date = "2024-01-14",
                Important = false,
                Category = "عام"
            },
            new {
                Id = 4,
                Title = "ورشة عمل تقنية",
                Content = "ورشة عمل حول تطوير التطبيقات باستخدام Flutter يوم السبت القادم",
                Date = "2024-01-18",
                Important = false,
                Category = "فعاليات"
            }
        };

        return Ok(announcements);
    }
}

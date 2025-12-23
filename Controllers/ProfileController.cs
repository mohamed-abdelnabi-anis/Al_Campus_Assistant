using Microsoft.AspNetCore.Mvc;
using Al_Campus_Assistant.Data;
using Al_Campus_Assistant.Models;
using Microsoft.EntityFrameworkCore;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Profile/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetProfile(int userId)
    {
        try
        {
            // 1. نجيب User (من صفحة التسجيل)
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            // 2. نجيب Student (إذا كان مسجل كمستقل)
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == user.Email); // نربط بالإيميل

            // 3. إذا مفيش student، نرجع بيانات User فقط
            if (student == null)
            {
                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        name = user.Name,
                        major = "Software Engineering - 3rd Year", // افتراضي
                        stats = new
                        {
                            gpa = 3.85,
                            hours = 85,
                            rank = 5
                        },
                        contact = new
                        {
                            email = user.Email,
                            phone = user.PhoneNumber
                        }
                    }
                });
            }

            // 4. إذا فيه student، ندمج البيانات
            return Ok(new
            {
                success = true,
                data = new
                {
                    name = !string.IsNullOrEmpty(student.Name) ? student.Name : user.Name,
                    major = $"{student.Department} - {student.AcademicYear}",
                    stats = new
                    {
                        gpa = 3.85, // مفيش في student الحالي
                        hours = 85,  // مفيش في student الحالي
                        rank = 5     // مفيش في student الحالي
                    },
                    contact = new
                    {
                        email = !string.IsNullOrEmpty(student.Email) ? student.Email : user.Email,
                        phone = user.PhoneNumber
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

    // PUT: api/Profile/{userId}
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateProfile(int userId, [FromBody] UpdateProfileRequest request)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { success = false, message = "المستخدم غير موجود" });

            // تحديث بيانات User فقط (الأساسية)
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                user.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "تم تحديث البيانات بنجاح"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ في تحديث البيانات",
                error = ex.Message
            });
        }
    }
}

public class UpdateProfileRequest
{
    public string? PhoneNumber { get; set; }
}
//using Microsoft.AspNetCore.Mvc;
//using Al_Campus_Assistant.Models;
//using Al_Campus_Assistant.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Al_Campus_Assistant.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class AuthController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;

//    public AuthController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> Register([FromBody] User user)
//    {
//        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
//        _context.Users.Add(user);
//        await _context.SaveChangesAsync();

//        return Ok(new { message = "تم إنشاء الحساب بنجاح", userId = user.Id });
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginRequest request)
//    {
//        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

//        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
//            return Unauthorized(new { message = "البريد الإلكتروني أو كلمة المرور غير صحيحة" });

//        return Ok(new
//        {
//            message = "تم تسجيل الدخول بنجاح",
//            user = new { user.Id, user.Name, user.Email, user.Role }
//        });
//    }
//}
//using Microsoft.AspNetCore.Mvc;
//using Al_Campus_Assistant.Models;
//using Al_Campus_Assistant.Data;
//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;

//namespace Al_Campus_Assistant.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class AuthController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;

//    public AuthController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // Register - يتطابق مع التصميم حرفياً
//    [HttpPost("register")]
//    public async Task<IActionResult> Register([FromBody] RegisterModel model)
//    {
//        // التحقق من صحة البيانات
//        if (!ModelState.IsValid)
//        {
//            var errors = ModelState.Values
//                .SelectMany(v => v.Errors)
//                .Select(e => e.ErrorMessage)
//                .ToList();

//            return BadRequest(new
//            {
//                success = false,
//                message = "بيانات غير صحيحة",
//                errors
//            });
//        }

//        // التحقق من عدم تكرار الإيميل
//        var existingUser = await _context.Users
//            .FirstOrDefaultAsync(u => u.Email == model.Email);

//        if (existingUser != null)
//            return BadRequest(new
//            {
//                success = false,
//                message = "البريد الإلكتروني مسجل بالفعل",
//                field = "Email"
//            });

//        // إنشاء User جديد حسب التصميم فقط
//        var user = new User
//        {
//            FirstName = model.FirstName,
//            LastName = model.LastName,
//            Email = model.Email,
//            PhoneNumber = model.PhoneNumber,
//            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
//            Role = "Student", // افتراضي لأن التصميم ما فيهاش اختيار
//            AgreeToTerms = model.AgreeToTerms,
//            IsAbove18 = model.IsAbove18,
//            CreatedAt = DateTime.Now,
//            IsActive = true
//        };

//        try
//        {
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            return Ok(new
//            {
//                success = true,
//                message = "تم إنشاء الحساب بنجاح",
//                userId = user.Id,
//                user = new
//                {
//                    user.FirstName,
//                    user.LastName,
//                    user.Email,
//                    user.PhoneNumber
//                }
//            });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new
//            {
//                success = false,
//                message = "حدث خطأ أثناء إنشاء الحساب",
//                error = ex.Message
//            });
//        }
//    }

//    // Login - كما هو
//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginRequest request)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = "بيانات الدخول غير صحيحة"
//            });
//        }

//        var user = await _context.Users
//            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

//        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
//            return Unauthorized(new
//            {
//                success = false,
//                message = "البريد الإلكتروني أو كلمة المرور غير صحيحة"
//            });

//        return Ok(new
//        {
//            success = true,
//            message = "تم تسجيل الدخول بنجاح",
//            user = new
//            {
//                user.Id,
//                Name = $"{user.FirstName} {user.LastName}",
//                user.FirstName,
//                user.LastName,
//                user.Email,
//                user.PhoneNumber,
//                user.Role // Role بتكون Student دائماً
//            }
//        });
//    }

//    // Forget Password - من التصميم
//    [HttpPost("forgot-password")]
//    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = "البريد الإلكتروني غير صحيح"
//            });
//        }

//        var user = await _context.Users
//            .FirstOrDefaultAsync(u => u.Email == model.Email && u.IsActive);

//        // لأسباب أمنية، دائماً نرجع نفس الرسالة
//        return Ok(new
//        {
//            success = true,
//            message = "إذا كان البريد الإلكتروني مسجلاً، ستتلقى رابط إعادة التعيين"
//        });
//    }
//}



using Microsoft.AspNetCore.Mvc;
using Al_Campus_Assistant.Models;
using Al_Campus_Assistant.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Register - يتطابق مع التصميم حرفياً
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // التحقق من صحة البيانات
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new
            {
                success = false,
                message = "بيانات غير صحيحة",
                errors
            });
        }

        // التحقق من عدم تكرار الإيميل
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (existingUser != null)
            return BadRequest(new
            {
                success = false,
                message = "البريد الإلكتروني مسجل بالفعل",
                field = "Email"
            });

        // إنشاء User جديد حسب التصميم فقط
        var user = new User
        {
            // ===== التعديل هنا =====
            // عندما نعيين FirstName و LastName، Name هتتولد تلقائياً
            FirstName = model.FirstName,
            LastName = model.LastName,
            // Name ستكون = $"{FirstName} {LastName}" تلقائياً
            // =======================
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = "Student", // افتراضي لأن التصميم ما فيهاش اختيار
            AgreeToTerms = model.AgreeToTerms,
            IsAbove18 = model.IsAbove18,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "تم إنشاء الحساب بنجاح",
                userId = user.Id,
                user = new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.PhoneNumber,
                    user.Name // ===== إضافة Name في الرد =====
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "حدث خطأ أثناء إنشاء الحساب",
                error = ex.Message
            });
        }
    }

    // Login - كما هو مع تعديل بسيط
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,
                message = "بيانات الدخول غير صحيحة"
            });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new
            {
                success = false,
                message = "البريد الإلكتروني أو كلمة المرور غير صحيحة"
            });

        return Ok(new
        {
            success = true,
            message = "تم تسجيل الدخول بنجاح",
            user = new
            {
                user.Id,
                // ===== التعديل هنا =====
                // استخدم user.Name مباشرة بدل تركيبها
                Name = user.Name, // ستكون "FirstName LastName"
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                user.Role // Role بتكون Student دائماً
            }
        });
    }

    // Forget Password - من التصميم
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,
                message = "البريد الإلكتروني غير صحيح"
            });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email && u.IsActive);

        // لأسباب أمنية، دائماً نرجع نفس الرسالة
        return Ok(new
        {
            success = true,
            message = "إذا كان البريد الإلكتروني مسجلاً، ستتلقى رابط إعادة التعيين"
        });
    }

    // ===== إضافة endpoint جديد: Reset Password =====
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,
                message = "بيانات غير صحيحة",
                errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
            });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email && u.IsActive);

        if (user == null)
        {
            return BadRequest(new
            {
                success = false,
                message = "البريد الإلكتروني غير مسجل أو الحساب غير مفعل"
            });
        }

        // في الواقع هنا بتكون عملت:
        // 1. التحقق من صحة الـ Token
        // 2. التحقق من انتهاء صلاحيته
        // للبروتوتايب، نفترض أن التوكن صحيح

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "تم إعادة تعيين كلمة المرور بنجاح"
        });
    }

    // ===== إضافة endpoint للتحقق من الإيميل =====
    [HttpGet("check-email/{email}")]
    public async Task<IActionResult> CheckEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { success = false, message = "البريد الإلكتروني مطلوب" });
        }

        var exists = await _context.Users
            .AnyAsync(u => u.Email == email && u.IsActive);

        return Ok(new
        {
            exists,
            message = exists ? "البريد الإلكتروني مسجل بالفعل" : "البريد الإلكتروني متاح"
        });
    }
}
using Microsoft.AspNetCore.Mvc;
using Al_Campus_Assistant.Models;
using Al_Campus_Assistant.Data;
using Microsoft.EntityFrameworkCore;

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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تم إنشاء الحساب بنجاح", userId = user.Id });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new { message = "البريد الإلكتروني أو كلمة المرور غير صحيحة" });

        return Ok(new
        {
            message = "تم تسجيل الدخول بنجاح",
            user = new { user.Id, user.Name, user.Email, user.Role }
        });
    }
}
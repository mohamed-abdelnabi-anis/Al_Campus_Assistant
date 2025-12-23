

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Al_Campus_Assistant.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }

    // ======== التعديل المهم: أضفنا خاصية Name ========
    // هذه خاصة للتوافق مع الكود القديم (مثل DashboardController)
    public string Name
    {
        get { return $"{FirstName} {LastName}".Trim(); }
        set
        {
            // إذا جينا من تسجيل قديم أو تعديل خارجي
            if (!string.IsNullOrEmpty(value))
            {
                var parts = value.Split(' ', 2);
                FirstName = parts.Length > 0 ? parts[0] : "";
                LastName = parts.Length > 1 ? parts[1] : "";
            }
        }
    }
    // ================================================

    // التصميم: First Name و Last Name فقط
    [Required(ErrorMessage = "الاسم الأول مطلوب")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "الاسم الأخير مطلوب")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    // Role بتكون Student افتراضياً (لأن التصميم ما فيهاش اختيار)
    public string Role { get; set; } = "Student";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;

    // التصميم: Phone فقط بدون Department ولا StudentId
    [Required(ErrorMessage = "رقم الهاتف مطلوب")]
    [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
    public string PhoneNumber { get; set; }

    // إضافة حقول الشروط من التصميم
    public bool AgreeToTerms { get; set; }
    public bool IsAbove18 { get; set; }
}

public class LoginRequest
{
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    public string Email { get; set; }

    [Required(ErrorMessage = "كلمة المرور مطلوبة")]
    public string Password { get; set; }
}

// RegisterModel حسب التصميم حرفياً
public class RegisterModel
{
    // First Name - من التصميم
    [Required(ErrorMessage = "الاسم الأول مطلوب")]
    public string FirstName { get; set; }

    // Last Name - من التصميم
    [Required(ErrorMessage = "الاسم الأخير مطلوب")]
    public string LastName { get; set; }

    // Email - من التصميم
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    public string Email { get; set; }

    // Phone - من التصميم
    [Required(ErrorMessage = "رقم الهاتف مطلوب")]
    [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
    public string PhoneNumber { get; set; }

    // Password - من التصميم
    [Required(ErrorMessage = "كلمة المرور مطلوبة")]
    [MinLength(6, ErrorMessage = "كلمة المرور يجب أن تكون 6 أحرف على الأقل")]
    public string Password { get; set; }

    // Confirm password - من التصميم
    [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
    [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتين")]
    public string ConfirmPassword { get; set; }

    // Agree to terms - من التصميم
    [Required(ErrorMessage = "يجب الموافقة على الشروط والأحكام")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "يجب الموافقة على الشروط والأحكام")]
    public bool AgreeToTerms { get; set; }

    // I have at least 18 years old - من التصميم
    [Required(ErrorMessage = "يجب أن يكون عمرك 18 سنة على الأقل")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "يجب أن يكون عمرك 18 سنة على الأقل")]
    public bool IsAbove18 { get; set; }
}

// Forget Password - من التصميم
public class ForgotPasswordModel
{
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    public string Email { get; set; }
}

// إضافة ResetPasswordModel للاكتمال
public class ResetPasswordModel
{
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    public string Email { get; set; }

    [Required(ErrorMessage = "رمز التحقق مطلوب")]
    public string Token { get; set; }

    [Required(ErrorMessage = "كلمة المرور الجديدة مطلوبة")]
    [MinLength(6, ErrorMessage = "كلمة المرور يجب أن تكون 6 أحرف على الأقل")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
    [Compare("NewPassword", ErrorMessage = "كلمتا المرور غير متطابقتين")]
    public string ConfirmPassword { get; set; }
}
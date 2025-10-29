namespace Al_Campus_Assistant.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // "Student", "Professor", "Admin"
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // الإضافات الجديدة ✅
    public bool IsActive { get; set; } = true;
    public string StudentId { get; set; } // رقم جامعي
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

namespace Al_Campus_Assistant.Models;

public class Professor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public string Office { get; set; }
    public string Phone { get; set; }
    public string OfficeHours { get; set; }

    // إضافة العلاقة مع الـCourses ✅
    public ICollection<Course> Courses { get; set; }
}
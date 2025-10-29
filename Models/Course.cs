//namespace Al_Campus_Assistant.Models;

//public class Course
//{
//    public int Id { get; set; }
//    public string Code { get; set; }
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public int CreditHours { get; set; }
//    public int ProfessorId { get; set; }
//}

namespace Al_Campus_Assistant.Models;

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CreditHours { get; set; }
    public int ProfessorId { get; set; }

    // إضافة العلاقة مع الـProfessor ✅
    public Professor Professor { get; set; }
    public ICollection<Exam> Exams { get; set; } // ✅ إضافة العلاقة مع الامتحانات
}
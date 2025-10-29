namespace Al_Campus_Assistant.Models;

public class Exam
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string ExamType { get; set; } // "منتصف الفصل" أو "نهائي"
    public DateTime ExamDate { get; set; }
    public string Time { get; set; }
    public string Location { get; set; }
    public int Duration { get; set; } // عدد الساعات

    // إضافة العلاقة مع الـCourse ✅
    public Course Course { get; set; }
}
using Al_Campus_Assistant.Models;
using Microsoft.EntityFrameworkCore;

namespace Al_Campus_Assistant.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // تأكد من أن قاعدة البيانات منشأة
        context.Database.EnsureCreated();

        // إضافة بيانات الدكاترة إذا كانت فارغة
        if (!context.Professors.Any())
        {
            var professors = new[]
            {
                new Professor { Name = "د. أحمد محمد", Department = "علوم الحاسب", Email = "ahmed@university.edu", Office = "مبنى أ - مكتب ٣٠١", Phone = "٠١٠٠٢٣٤٥٦٧٨", OfficeHours = "الإثنين والأربعاء - ١٠ صباحاً إلى ١٢ ظهراً" },
                new Professor { Name = "د. مريم عبدالله", Department = "هندسة البرمجيات", Email = "mariam@university.edu", Office = "مبنى ب - مكتب ٢٠٥", Phone = "٠١٠٩٨٧٦٥٤٣٢", OfficeHours = "الثلاثاء والخميس - ٩ صباحاً إلى ١١ صباحاً" }
            };
            context.Professors.AddRange(professors);
            context.SaveChanges();
        }

        // إضافة مواد دراسية إذا كانت فارغة
        if (!context.Courses.Any())
        {
            var courses = new[]
            {
                new Course { Code = "CS101", Name = "برمجة التطبيقات", Description = "مقدمة في برمجة التطبيقات باستخدام C#", CreditHours = 3, ProfessorId = 1 },
                new Course { Code = "CS102", Name = "قواعد البيانات", Description = "تصميم وإدارة قواعد البيانات", CreditHours = 3, ProfessorId = 2 },
                new Course { Code = "CS201", Name = "هندسة البرمجيات", Description = "مبادئ هندسة البرمجيات وتطوير الأنظمة", CreditHours = 4, ProfessorId = 1 }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }

        // إضافة امتحانات إذا كانت فارغة
        if (!context.Exams.Any())
        {
            var exams = new[]
            {
                new Exam { CourseId = 1, ExamType = "منتصف الفصل", ExamDate = new DateTime(2024, 12, 15), Time = "10:00 AM", Location = "قاعة 101", Duration = 3 },
                new Exam { CourseId = 2, ExamType = "منتصف الفصل", ExamDate = new DateTime(2024, 12, 17), Time = "01:00 PM", Location = "قاعة 202", Duration = 2 },
                new Exam { CourseId = 1, ExamType = "نهائي", ExamDate = new DateTime(2025, 1, 10), Time = "09:00 AM", Location = "قاعة 305", Duration = 3 }
            };
            context.Exams.AddRange(exams);
            context.SaveChanges();
        }

        // إضافة إعلانات إذا كانت فارغة
        if (!context.Announcements.Any())
        {
            var announcements = new[]
            {
                new Announcement { Title = "تغيير موعد محاضرة", Content = "تم تأجيل محاضرة قواعد بيانات إلى يوم الأربعاء الساعة 10 صباحاً", Date = new DateTime(2024, 1, 16), IsImportant = true, Category = "مهم" },
                new Announcement { Title = "نتائج الامتحانات", Content = "ظهرت نتائج امتحانات منتصف الفصل، يمكن الاطلاع عليها من خلال النظام", Date = new DateTime(2024, 1, 15), IsImportant = true, Category = "مهم" }
            };
            context.Announcements.AddRange(announcements);
            context.SaveChanges();
        }
    }
}
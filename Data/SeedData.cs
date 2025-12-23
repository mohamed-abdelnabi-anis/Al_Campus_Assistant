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

//using Microsoft.EntityFrameworkCore;
//using Al_Campus_Assistant.Models;
//using Al_Campus_Assistant.Data;
//namespace Al_Campus_Assistant.Data;
//public static class SeedData
//{
//    private static readonly Random _random = new Random();

//    public static void Initialize(ApplicationDbContext context)
//    {
//        if (context.Users.Any())
//        {
//            Console.WriteLine("✅ قاعدة البيانات فيها بيانات بالفعل");
//            return;
//        }

//        Console.WriteLine("🚀 بدء إضافة البيانات الضخمة...");

//        #region 1. إنشاء 100 مستخدم
//        var users = new List<User>();

//        // 5 أدمن
//        for (int i = 1; i <= 5; i++)
//        {
//            users.Add(CreateAdminUser(i));
//        }

//        // 25 أستاذ
//        for (int i = 1; i <= 25; i++)
//        {
//            users.Add(CreateProfessorUser(i));
//        }

//        // 70 طالب
//        for (int i = 1; i <= 70; i++)
//        {
//            users.Add(CreateStudentUser(i));
//        }

//        context.Users.AddRange(users);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {users.Count} مستخدم");
//        #endregion

//        #region 2. إنشاء 25 أستاذ (Professors)
//        var professors = new List<Professor>();
//        var departments = new[] { "Computer Science", "Software Engineering", "Information Systems",
//                                  "Mathematics", "Physics", "Chemistry", "Biology",
//                                  "Civil Engineering", "Electrical Engineering", "Mechanical Engineering" };

//        var professorNames = new[]
//        {
//            "Dr. Ahmed Mohamed", "Dr. Sara Khalid", "Dr. Omar Ali", "Dr. Fatima Hassan", "Dr. Khaled Ibrahim",
//            "Dr. Nora Salem", "Dr. Youssef Reda", "Dr. Lina Kamal", "Dr. Mahmoud Abdullah", "Dr. Huda Saeed",
//            "Dr. Basel Nasser", "Dr. Reem Walid", "Dr. Tariq Farouk", "Dr. Nadia Jameel", "Dr. Wesam Adel",
//            "Dr. Mona Samir", "Dr. Rami Tawfik", "Dr. Dina Karim", "Dr. Sami Rashid", "Dr. Layla Majid",
//            "Dr. Ziad Salem", "Dr. Rana Younis", "Dr. Fadi Nader", "Dr. Maya Hani", "Dr. Karim Fawzi"
//        };

//        for (int i = 0; i < 25; i++)
//        {
//            professors.Add(new Professor
//            {
//                Name = professorNames[i],
//                Email = $"professor{i + 1}@university.edu",
//                Department = departments[i % departments.Length],
//                Office = $"Building {(i % 5) + 1} - Room {200 + i}",
//                Phone = $"0552{10000 + i:00000}",
//                OfficeHours = $"{9 + (i % 4)}:00 AM - {1 + (i % 4)}:00 PM",
//                Courses = new List<Course>()
//            });
//        }

//        context.Professors.AddRange(professors);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {professors.Count} أستاذ");
//        #endregion

//        #region 3. إنشاء 70 طالب (Students)
//        var students = new List<Student>();
//        var studentFirstNames = new[] { "Ahmed", "Mohamed", "Ali", "Khaled", "Omar", "Youssef", "Mahmoud", "Basel", "Tariq", "Wesam" };
//        var studentLastNames = new[] { "Al-Fath", "Al-Nasr", "Al-Khair", "Al-Baraka", "Al-Najah", "Al-Tawfiq", "Al-Falah", "Al-Saada", "Al-Tamayoz", "Al-Ibdaa" };
//        var academicYears = new[] { "First Year", "Second Year", "Third Year", "Fourth Year" };
//        var studentMajors = new[] { "Computer Science", "Software Engineering", "Information Technology",
//                                    "Computer Engineering", "Electrical Engineering", "Mechanical Engineering" };

//        for (int i = 1; i <= 70; i++)
//        {
//            var firstName = studentFirstNames[_random.Next(studentFirstNames.Length)];
//            var lastName = studentLastNames[_random.Next(studentLastNames.Length)];

//            students.Add(new Student
//            {
//                Name = $"{firstName} {lastName}",
//                Email = $"student{i}@student.edu",
//                PasswordHash = BCrypt.Net.BCrypt.HashPassword($"Student{i}@123"),
//                Department = studentMajors[_random.Next(studentMajors.Length)],
//                AcademicYear = academicYears[_random.Next(academicYears.Length)],
//                CreatedAt = DateTime.Now.AddDays(-_random.Next(1, 365))
//            });
//        }

//        context.Students.AddRange(students);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {students.Count} طالب");
//        #endregion

//        #region 4. إنشاء 40 مقرر (Courses)
//        var courses = new List<Course>();
//        var courseCodes = new[] { "CS", "SE", "IT", "MATH", "PHYS", "CHEM", "BIO", "CE", "EE", "ME" };
//        var courseNames = new[]
//        {
//            "Introduction to Programming", "Data Structures", "Algorithms", "Database Systems",
//            "Computer Networks", "Operating Systems", "Software Engineering", "Web Development",
//            "Mobile Application Development", "Artificial Intelligence", "Machine Learning",
//            "Computer Graphics", "Cybersecurity", "Cloud Computing", "Data Science",
//            "Calculus I", "Calculus II", "Linear Algebra", "Discrete Mathematics",
//            "Physics I", "Physics II", "Chemistry I", "Biology I",
//            "Circuit Analysis", "Digital Logic Design", "Thermodynamics", "Fluid Mechanics"
//        };

//        var courseDescriptions = new[]
//        {
//            "Fundamental concepts of programming and problem-solving",
//            "Study of data organization and management techniques",
//            "Design and analysis of efficient algorithms",
//            "Principles of database design and implementation",
//            "Network architectures, protocols, and technologies",
//            "Operating system concepts and implementation",
//            "Software development methodologies and practices",
//            "Building modern web applications using latest technologies",
//            "Developing mobile apps for iOS and Android platforms",
//            "Introduction to AI concepts and applications",
//            "Machine learning algorithms and applications",
//            "Computer graphics principles and rendering techniques",
//            "Security principles and protection mechanisms",
//            "Cloud computing models and deployment",
//            "Data analysis and visualization techniques"
//        };

//        for (int i = 1; i <= 40; i++)
//        {
//            var code = $"{courseCodes[i % courseCodes.Length]}{100 + i}";
//            var name = courseNames[i % courseNames.Length];
//            var professor = professors[_random.Next(professors.Count)];

//            courses.Add(new Course
//            {
//                Code = code,
//                Name = name,
//                Description = courseDescriptions[i % courseDescriptions.Length] + $" - Course code: {code}",
//                CreditHours = _random.Next(2, 4), // 2 or 3 credit hours
//                ProfessorId = professor.Id,
//                Professor = professor
//            });
//        }

//        context.Courses.AddRange(courses);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {courses.Count} مقرر");
//        #endregion

//        #region 5. إنشاء 80 امتحان (Exams)
//        var exams = new List<Exam>();
//        var examTypes = new[] { "Midterm", "Final", "Quiz", "Assignment" };
//        var examLocations = new[] { "Hall 101", "Hall 102", "Hall 201", "Hall 202", "Hall 301",
//                                    "Lab 1", "Lab 2", "Lab 3", "Lab 4", "Online" };
//        var examTimes = new[] { "9:00 AM", "10:30 AM", "12:00 PM", "1:30 PM", "3:00 PM", "4:30 PM" };

//        for (int i = 1; i <= 80; i++)
//        {
//            var course = courses[_random.Next(courses.Count)];
//            var examType = examTypes[_random.Next(examTypes.Length)];

//            exams.Add(new Exam
//            {
//                CourseId = course.Id,
//                ExamType = examType,

//                ExamDate = DateTime.Now.AddDays(_random.Next(1, 60)), // امتحانات خلال 60 يوم
//                Time = examTimes[_random.Next(examTimes.Length)],
//                Location = examLocations[_random.Next(examLocations.Length)],
//                Duration = examType == "Quiz" ? 60 : (examType == "Midterm" ? 120 : 180),
//                Course = course
//            });
//        }

//        context.Exams.AddRange(exams);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {exams.Count} امتحان");
//        #endregion

//        #region 6. إنشاء 50 إعلان (Announcements)
//        var announcements = new List<Announcement>();
//        var announcementCategories = new[] { "Academic", "Event", "General", "Important", "Urgent" };
//        var announcementTitles = new[]
//        {
//            "Exam Schedule Released", "Lecture Cancelled", "New Course Offering",
//            "Workshop Announcement", "Library Hours Changed", "Holiday Announcement",
//            "Scholarship Opportunity", "Internship Program", "Research Assistant Needed",
//            "Sports Event", "Cultural Festival", "Career Fair", "Guest Lecture",
//            "Thesis Defense", "Graduation Ceremony", "Registration Reminder",
//            "Payment Deadline", "Academic Calendar Update", "Faculty Meeting",
//            "Student Council Election"
//        };

//        var announcementContents = new[]
//        {
//            "The schedule for final exams has been published on the university portal.",
//            "The lecture scheduled for tomorrow has been cancelled due to unforeseen circumstances.",
//            "We are pleased to announce a new course offering for the next semester.",
//            "Join us for an interactive workshop on the latest technologies.",
//            "Please note the change in library operating hours effective next week.",
//            "The university will be closed for the upcoming national holiday.",
//            "Applications are now open for the merit-based scholarship program.",
//            "Exciting internship opportunities are available with leading companies.",
//            "The department is looking for research assistants for ongoing projects.",
//            "Don't miss the annual sports competition next month."
//        };

//        for (int i = 1; i <= 50; i++)
//        {
//            announcements.Add(new Announcement
//            {
//                Title = announcementTitles[i % announcementTitles.Length],
//                Content = announcementContents[i % announcementContents.Length] +
//                         $" This announcement was published on {DateTime.Now.AddDays(-_random.Next(30)):yyyy-MM-dd}.",
//                Date = DateTime.Now.AddDays(-_random.Next(30)), // إعلانات خلال 30 يوم
//                IsImportant = _random.Next(5) == 0, // 20% مهمة
//                Category = announcementCategories[_random.Next(announcementCategories.Length)]
//            });
//        }

//        context.Announcements.AddRange(announcements);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {announcements.Count} إعلان");
//        #endregion

//        #region 7. إنشاء 150 إشعار (Notifications)
//        var notifications = new List<Notification>();
//        var notificationTypes = new[] { "info", "warning", "success", "emergency" };
//        var notificationTitles = new[]
//        {
//            "New Message", "Grade Posted", "Assignment Due", "Lecture Reminder",
//            "Exam Tomorrow", "Payment Due", "Event Starting Soon", "System Maintenance",
//            "Security Alert", "Welcome Notification", "Course Update", "Schedule Change"
//        };

//        var notificationMessages = new[]
//        {
//            "You have a new message from your professor.",
//            "Your grade for the recent exam has been posted.",
//            "Reminder: Assignment is due tomorrow.",
//            "Your next lecture starts in 30 minutes.",
//            "Don't forget: Exam is scheduled for tomorrow.",
//            "Reminder: Tuition payment is due next week.",
//            "The event you registered for starts in 1 hour.",
//            "System maintenance scheduled for tonight.",
//            "Important security update required.",
//            "Welcome to the campus portal system!",
//            "There has been an update to your course materials.",
//            "There is a change in your class schedule."
//        };

//        for (int i = 1; i <= 150; i++)
//        {
//            var userId = _random.Next(1, 100); // IDs من 1 إلى 100

//            notifications.Add(new Notification
//            {
//                Title = notificationTitles[i % notificationTitles.Length],
//                Message = notificationMessages[i % notificationMessages.Length],
//                Type = notificationTypes[_random.Next(notificationTypes.Length)],
//                UserId = userId,
//                IsRead = _random.Next(3) == 0, // 33% مقروءة
//                CreatedAt = DateTime.Now.AddHours(-_random.Next(1, 168)), // خلال أسبوع
//                ExpiresAt = _random.Next(5) == 0 ? DateTime.Now.AddDays(7) : null // 20% تنتهي
//            });
//        }

//        context.Notifications.AddRange(notifications);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {notifications.Count} إشعار");
//        #endregion

//        #region 8. إنشاء إعدادات للمستخدمين (UserSettings)
//        var userSettings = new List<UserSettings>();
//        var languages = new[] { "العربية", "English", "Français", "Español" };

//        // إعدادات لـ 30 مستخدم
//        for (int i = 1; i <= 30; i++)
//        {
//            userSettings.Add(new UserSettings
//            {
//                UserId = i,
//                Language = languages[_random.Next(languages.Length)],
//                DarkMode = _random.Next(2) == 0, // 50% dark mode
//                LectureAlerts = _random.Next(4) != 0, // 75% تشغيل
//                ExamAlerts = _random.Next(4) != 0, // 75% تشغيل
//                AnnouncementAlerts = _random.Next(4) != 0, // 75% تشغيل
//                EmailNotifications = _random.Next(3) == 0, // 33% تشغيل
//                PushNotifications = _random.Next(4) != 0, // 75% تشغيل
//                FontSize = _random.Next(3) switch { 0 => "small", 1 => "medium", _ => "large" },
//                Vibration = _random.Next(4) != 0, // 75% تشغيل
//                Sound = _random.Next(4) != 0, // 75% تشغيل
//                CreatedAt = DateTime.Now.AddDays(-_random.Next(1, 30)),
//                UpdatedAt = DateTime.Now.AddDays(-_random.Next(1, 7))
//            });
//        }

//        context.UserSettings.AddRange(userSettings);
//        context.SaveChanges();
//        Console.WriteLine($"✅ تم إنشاء {userSettings.Count} إعدادات مستخدم");
//        #endregion

//        Console.WriteLine("🎉 تم الانتهاء من إضافة جميع البيانات الضخمة!");
//        Console.WriteLine("📊 الإحصائيات النهائية:");
//        Console.WriteLine($"   👥 المستخدمون: {users.Count}");
//        Console.WriteLine($"   👨‍🏫 الأساتذة: {professors.Count}");
//        Console.WriteLine($"   👨‍🎓 الطلاب: {students.Count}");
//        Console.WriteLine($"   📚 المقررات: {courses.Count}");
//        Console.WriteLine($"   📝 الامتحانات: {exams.Count}");
//        Console.WriteLine($"   📢 الإعلانات: {announcements.Count}");
//        Console.WriteLine($"   🔔 الإشعارات: {notifications.Count}");
//        Console.WriteLine($"   ⚙️ الإعدادات: {userSettings.Count}");
//        Console.WriteLine($"   📈 المجموع: {users.Count + professors.Count + students.Count + courses.Count + exams.Count + announcements.Count + notifications.Count + userSettings.Count} سجل");
//    }

//    #region دوال مساعدة لإنشاء المستخدمين
//    private static User CreateAdminUser(int index)
//    {
//        return new User
//        {
//            FirstName = "Admin",
//            LastName = $"System{index}",
//            Email = $"admin{index}@university.edu",
//            PhoneNumber = $"055100{index:0000}",
//            PasswordHash = BCrypt.Net.BCrypt.HashPassword($"Admin{index}@123"),
//            Role = "Admin",
//            CreatedAt = DateTime.Now.AddDays(-index * 7),
//            IsActive = true,
//            AgreeToTerms = true,
//            IsAbove18 = true
//        };
//    }

//    private static User CreateProfessorUser(int index)
//    {
//        var profNames = new[] { "Ahmed", "Sara", "Omar", "Fatima", "Khaled", "Nora", "Youssef", "Lina", "Mahmoud", "Huda" };
//        var profLastNames = new[] { "Mohamed", "Khalid", "Ali", "Hassan", "Ibrahim", "Salem", "Reda", "Kamal", "Abdullah", "Saeed" };

//        return new User
//        {
//            FirstName = profNames[index % profNames.Length],
//            LastName = profLastNames[index % profLastNames.Length],
//            Email = $"professor{index}@university.edu",
//            PhoneNumber = $"055200{index:0000}",
//            PasswordHash = BCrypt.Net.BCrypt.HashPassword($"Professor{index}@123"),
//            Role = "Professor",
//            CreatedAt = DateTime.Now.AddDays(-index * 3),
//            IsActive = index <= 23, // 2 أساتذة غير نشطين
//            AgreeToTerms = true,
//            IsAbove18 = true
//        };
//    }

//    private static User CreateStudentUser(int index)
//    {
//        var studentFirstNames = new[] { "Ahmed", "Mohamed", "Ali", "Khaled", "Omar", "Youssef", "Mahmoud", "Basel", "Tariq", "Wesam" };
//        var studentLastNames = new[] { "Al-Fath", "Al-Nasr", "Al-Khair", "Al-Baraka", "Al-Najah", "Al-Tawfiq", "Al-Falah", "Al-Saada", "Al-Tamayoz", "Al-Ibdaa" };

//        return new User
//        {
//            FirstName = studentFirstNames[index % studentFirstNames.Length],
//            LastName = studentLastNames[index % studentLastNames.Length],
//            Email = $"student{index}@student.edu",
//            PhoneNumber = $"055300{index:0000}",
//            PasswordHash = BCrypt.Net.BCrypt.HashPassword($"Student{index}@123"),
//            Role = "Student",
//            CreatedAt = DateTime.Now.AddDays(-index),
//            IsActive = index <= 65, // 5 طلاب غير نشطين
//            AgreeToTerms = true,
//            IsAbove18 = true
//        };
//    }
//    #endregion
//}
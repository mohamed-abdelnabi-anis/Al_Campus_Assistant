

//namespace AI_Campus_Assistant.Controllers
//{
//    public class CoursesController
//    {
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Al_Campus_Assistant.Data;
using Al_Campus_Assistant.Models;
using Microsoft.EntityFrameworkCore;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Courses - جلب جميع المواد
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _context.Courses
            .Include(c => c.Professor)
            .Select(c => new
            {
                c.Id,
                c.Code,
                c.Name,
                c.Description,
                c.CreditHours,
                ProfessorName = c.Professor.Name,
                c.ProfessorId
            })
            .ToListAsync();

        return Ok(courses);
    }

    // GET: api/Courses/5 - جلب مادة by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Professor)
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.Id,
                c.Code,
                c.Name,
                c.Description,
                c.CreditHours,
                ProfessorName = c.Professor.Name,
                ProfessorEmail = c.Professor.Email,
                ProfessorOffice = c.Professor.Office,
                c.ProfessorId
            })
            .FirstOrDefaultAsync();

        if (course == null)
        {
            return NotFound(new { message = "المادة غير موجودة" });
        }

        return Ok(course);
    }

    // POST: api/Courses - إضافة مادة جديدة
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تم إضافة المادة الدراسية بنجاح", courseId = course.Id });
    }

    // PUT: api/Courses/5 - تعديل مادة
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course updatedCourse)
    {
        if (id != updatedCourse.Id)
        {
            return BadRequest(new { message = "رقم المادة غير متطابق" });
        }

        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound(new { message = "المادة غير موجودة" });
        }

        course.Name = updatedCourse.Name;
        course.Code = updatedCourse.Code;
        course.Description = updatedCourse.Description;
        course.CreditHours = updatedCourse.CreditHours;
        course.ProfessorId = updatedCourse.ProfessorId;

        await _context.SaveChangesAsync();

        return Ok(new { message = "تم تحديث المادة الدراسية بنجاح" });
    }

    // DELETE: api/Courses/5 - حذف مادة
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound(new { message = "المادة غير موجودة" });
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تم حذف المادة الدراسية بنجاح" });
    }

    // GET: api/Courses/professor/5 - جلب مواد أستاذ معين
    [HttpGet("professor/{professorId}")]
    public async Task<IActionResult> GetCoursesByProfessor(int professorId)
    {
        var courses = await _context.Courses
            .Where(c => c.ProfessorId == professorId)
            .Select(c => new
            {
                c.Id,
                c.Code,
                c.Name,
                c.Description,
                c.CreditHours
            })
            .ToListAsync();

        return Ok(courses);
    }
}
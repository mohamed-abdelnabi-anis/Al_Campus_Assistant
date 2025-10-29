using Microsoft.AspNetCore.Mvc;

namespace Al_Campus_Assistant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProfessors()
    {
        var professors = new[]
        {
            new {
                Id = 1,
                Name = "د. أحمد محمد",
                Department = "علوم الحاسب",
                Email = "ahmed@university.edu",
                Office = "مبنى أ - مكتب ٣٠١",
                Phone = "٠١٠٠٢٣٤٥٦٧٨",
                OfficeHours = "الإثنين والأربعاء - ١٠ صباحاً إلى ١٢ ظهراً"
            },
            new {
                Id = 2,
                Name = "د. مريم عبدالله",
                Department = "هندسة البرمجيات",
                Email = "mariam@university.edu",
                Office = "مبنى ب - مكتب ٢٠٥",
                Phone = "٠١٠٩٨٧٦٥٤٣٢",
                OfficeHours = "الثلاثاء والخميس - ٩ صباحاً إلى ١١ صباحاً"
            },
            new {
                Id = 3,
                Name = "د. خالد مصطفى",
                Department = "قواعد البيانات",
                Email = "khaled@university.edu",
                Office = "مبنى ج - مكتب ١٠٥",
                Phone = "٠١٠١١٢٢٣٣٤٤",
                OfficeHours = "الأحد والثلاثاء - ٢ ظهراً إلى ٤ عصراً"
            }
        };

        return Ok(professors);
    }
}
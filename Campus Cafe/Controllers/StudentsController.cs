using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Campus_Cafe.Data;

namespace Campus_Cafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly CampusCafeDBContext _context;

        public StudentsController(CampusCafeDBContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchStudents(string? name)
        {
            // QUESTION FOR TEACHER: If ?name is missing or empty, should this return
            // all students or an empty list? The endpoint table only specifies 200 OK.

            string search = (name ?? "").Trim().ToLower();

            var students = await _context.Students
                .Where(student =>
                    student.FullName.ToLower().Contains(search)
                    ||
                    student.CardNumber.ToLower().Contains(search))
                .Select(student => new
                {
                    student.StudentId,
                    student.FullName,
                    student.CardNumber,
                    student.Grade,
                    student.DiscountPercent
                })
                .ToListAsync();

            return Ok(students);
        }


    }

}
   

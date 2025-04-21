using ManyWhoCan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManyWhoCan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TeacherController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAll()
        {
            return await _context.Teachers
                .Include(t => t.Courses)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.TeacherId == id);

            if (teacher == null) return NotFound();

            return teacher;
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = teacher.TeacherId }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId) return BadRequest();

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

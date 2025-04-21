using ManyWhoCan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManyWhoCan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CoursesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null) return NotFound();

            return course;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Create(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course course)
        {
            if (id != course.CourseId) return BadRequest();

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

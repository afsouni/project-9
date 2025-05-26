using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationContext _context;
    public StudentsController(ApplicationContext context) => _context = context;

    [HttpGet]
    public async Task<IEnumerable<Student>> Get()
    {
        // Return all students
        return await _context.Students.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Post(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

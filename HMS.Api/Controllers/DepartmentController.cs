using HMS.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentDbContext _context;

    public DepartmentController(DepartmentDbContext context)
    {
        _context = context;
    }

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments.ToListAsync();
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
            return NotFound();

        return department;
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, Department department)
    {
        if (id != department.Id)
            return BadRequest();

        _context.Entry(department).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Departments.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
            return NotFound();

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
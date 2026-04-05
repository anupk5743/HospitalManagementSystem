using HMS.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public DoctorController(DepartmentDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await _context.Doctors
                .Include(d => d.Department)
                .ToListAsync();

            return Ok(doctors);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                return NotFound("Doctor not found");

            return Ok(doctor);
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.Id)
                return BadRequest("Id mismatch");

            var existingDoctor = await _context.Doctors.FindAsync(id);
            if (existingDoctor == null)
                return NotFound("Doctor not found");

            existingDoctor.Name = doctor.Name;
            existingDoctor.DepartmentId = doctor.DepartmentId;
            existingDoctor.Specialization = doctor.Specialization;

            await _context.SaveChangesAsync();

            return Ok(existingDoctor);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return NotFound("Doctor not found");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return Ok("Doctor deleted successfully");
        }
    }
}
using AutoMapper;
using HMS.Api.Models;
using HMS.API.DTOs;
using HMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly DepartmentDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentsController(DepartmentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

        // GET ALL
        [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ThenInclude(d => d.Department)
            .ToListAsync();

        return Ok(_mapper.Map<List<AppointmentDTO>>(appointments));
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ThenInclude(d => d.Department)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
            return NotFound("Appointment not found");

        return Ok(_mapper.Map<AppointmentDTO>(appointment));
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var patient = await _context.Patients.FindAsync(appointment.PatientId);
        if (patient == null)
            return BadRequest("Invalid PatientId");

        var doctor = await _context.Doctors.FindAsync(appointment.DoctorId);
        if (doctor == null)
            return BadRequest("Invalid DoctorId");

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
    {
        if (id != appointment.Id)
            return BadRequest("Id mismatch");

        var existing = await _context.Appointments.FindAsync(id);
        if (existing == null)
            return NotFound("Appointment not found");

        existing.PatientId = appointment.PatientId;
        existing.DoctorId = appointment.DoctorId;
        existing.AppointmentDate = appointment.AppointmentDate;
        existing.Symptoms = appointment.Symptoms;
        existing.Status = appointment.Status;   

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
            return NotFound("Appointment not found");

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return Ok("Appointment deleted successfully");
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS.Api.Models;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly DepartmentDbContext _context;

    public ReportsController(DepartmentDbContext context)
    {
        _context = context;
    }

    // Patient Report
    [HttpGet("patients")]
    public async Task<IActionResult> PatientReport()
    {
        var data = await _context.Patients.ToListAsync();

        return Ok(data);
    }

    // Appointment Report
    [HttpGet("appointments")]
    public async Task<IActionResult> AppointmentReport()
    {
        var data = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync();

        return Ok(data);
    }

    // Revenue Report
    [HttpGet("revenue")]
    public IActionResult RevenueReport()
    {
        var revenue = _context.Billings.Sum(b => b.TotalAmount);

        return Ok(new { TotalRevenue = revenue });
    }
}
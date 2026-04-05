using HMS.Api.DTOs;
using HMS.Api.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly DepartmentDbContext _context;

    public DashboardController(DepartmentDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetStats()
    {
        var stats = new DashboardsDTO
        {
            TotalPatients = _context.Patients.Count(),
            TotalDoctors = _context.Doctors.Count(),
            TotalAppointments = _context.Appointments.Count(),
            TotalDepartments = _context.Departments.Count()
        };

        return Ok(stats);
    }
}
using HMS.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS.Api.Models;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardAnalyticsController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public DashboardAnalyticsController(DepartmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("analytics")]
        public IActionResult GetAnalytics()
        {
            var months = _context.Appointments
                .GroupBy(a => a.AppointmentDate.Month)
                .Select(g => g.Key)
                .OrderBy(m => m)
                .ToList();

            var appointmentCounts = _context.Appointments
                .GroupBy(a => a.AppointmentDate.Month)
                .Select(g => g.Count())
                .ToList();

            var revenue = _context.Billings
                .Include(b => b.Appointment)
                .GroupBy(b => b.Appointment.AppointmentDate.Month)
                .Select(g => g.Sum(b => b.TotalAmount))
                .ToList();

            var data = new DashboardAnalyticsDTO
            {
                Months = months.Select(m =>
                    System.Globalization.CultureInfo.CurrentCulture
                    .DateTimeFormat.GetMonthName(m)).ToList(),

                AppointmentCounts = appointmentCounts,
                Revenue = revenue
            };

            return Ok(data);
        }
    }
}
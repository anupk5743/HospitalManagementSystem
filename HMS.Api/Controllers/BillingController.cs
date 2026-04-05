using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS.API.Models;
using HMS.Api.Models;

[Route("api/[controller]")]
[ApiController]
public class BillingController : ControllerBase
{
    private readonly DepartmentDbContext _context;

    public BillingController(DepartmentDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetBills()
    {
        var data = await _context.Billings.ToListAsync();
        return Ok(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBill(int id)
    {
        var bill = await _context.Billings.FindAsync(id);

        if (bill == null)
            return NotFound();

        return Ok(bill);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBill(Billing bill)
    {
        bill.TotalAmount =
            bill.DoctorFee +
            bill.MedicineCharges +
            bill.LabCharges;

        _context.Billings.Add(bill);
        await _context.SaveChangesAsync();

        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBill(int id, Billing bill)
    {
        var existing = await _context.Billings.FindAsync(id);

        if (existing == null)
            return NotFound();

        existing.DoctorFee = bill.DoctorFee;
        existing.MedicineCharges = bill.MedicineCharges;
        existing.LabCharges = bill.LabCharges;
        existing.PaymentStatus = bill.PaymentStatus;

        existing.TotalAmount =
            bill.DoctorFee +
            bill.MedicineCharges +
            bill.LabCharges;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBill(int id)
    {
        var bill = await _context.Billings.FindAsync(id);

        if (bill == null)
            return NotFound();

        _context.Billings.Remove(bill);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
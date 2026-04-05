using Microsoft.AspNetCore.Mvc;
using HMS.Web.Models;
using System.Net.Http.Json;

public class ReportsController : Controller
{
    private readonly HttpClient _client;

    public ReportsController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://localhost:44382/Api/");
    }

    public async Task<IActionResult> Patients()
    {
        var data = await _client.GetFromJsonAsync<List<PatientVM>>("Reports/Patients");

        return View(data);
    }

    public async Task<IActionResult> Appointments()
    {
        var data = await _client.GetFromJsonAsync<List<AppointmentVM>>("Reports/Appointments");

        return View(data);
    }

    public async Task<IActionResult> Revenue()
    {
        var data = await _client.GetFromJsonAsync<RevenueReportVM>("Reports/Revenue");

        return View(data);
    }
}
using Microsoft.AspNetCore.Mvc;
using HMS.Web.Models;
using System.Net.Http.Json;
using HMS.Web.Filters;

public class DashboardController : Controller
{
    private readonly HttpClient _client;

    public DashboardController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://localhost:44382/Api/");
    }

    public async Task<IActionResult> Index()
    {
        var stats = await _client.GetFromJsonAsync<DashboardVM>("dashboard");

        return View(stats);
    }
}
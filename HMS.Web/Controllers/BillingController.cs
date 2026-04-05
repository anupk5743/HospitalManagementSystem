using HMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

public class BillingController : Controller
{
    private readonly HttpClient _client;

    public BillingController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://localhost:44382/Api/");
    }

    public async Task<IActionResult> Index()
    {
        var bills = await _client.GetFromJsonAsync<List<BillingVM>>("Billing");
        return View(bills);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BillingVM bill)
    {
        await _client.PostAsJsonAsync("Billing", bill);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var bill = await _client.GetFromJsonAsync<BillingVM>($"Billing/{id}");
        return View(bill);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, BillingVM bill)
    {
        await _client.PutAsJsonAsync($"Billing/{id}", bill);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _client.DeleteAsync($"Billing/{id}");
        return RedirectToAction("Index");
    }
}
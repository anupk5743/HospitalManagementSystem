using HMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

public class AccountController : Controller
{
    private readonly HttpClient _client;


    public AccountController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://localhost:44382/Api/");
    }

    // LOGIN GET
    public IActionResult Login()
    {
        return View();
    }

    // LOGIN POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login)
    {
        var response = await _client.PostAsJsonAsync("Auth/Login", login);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Invalid Login";
            return View();
        }

        var token = await response.Content.ReadAsStringAsync();

        HttpContext.Session.SetString("JWT", token);
        HttpContext.Session.SetString("UserName", login.Username);

        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        var response = await _client.PostAsJsonAsync("Auth/Register", register);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Registration Failed";
            return View(register);
        }

        return RedirectToAction("Login");
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }
}
using Microsoft.AspNetCore.Mvc;
using HMS.Web.Models;
using System.Net.Http.Json;

namespace HMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44382/Api/");
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = await _client.GetFromJsonAsync<DashboardVM>
            ("Dashboard", new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(dashboard);
        }
    }
}
using HMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace HMS.Web.Controllers
{
    public class DashboardAnalyticsController : Controller
    {
        private readonly HttpClient _client;

        public DashboardAnalyticsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44382/Api/");
        }

        public async Task<IActionResult> Index()
        {
            var stats = await _client.GetFromJsonAsync<DashboardVM>("dashboard");

            var analytics = await _client
                .GetFromJsonAsync<DashboardAnalyticsVM>("DashboardAnalytics/analytics");

            ViewBag.Analytics = analytics;

            return View(stats);
        }
    }
}
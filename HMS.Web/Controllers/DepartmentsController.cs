using Microsoft.AspNetCore.Mvc;
using HMS.Web.Models;
using System.Net.Http.Json;
using HMS.Web.Filters;

namespace HMS.Web.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class DepartmentsController : Controller
    {
        private readonly HttpClient _client;

        public DepartmentsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44382/Api/");
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<DepartmentVM>>("Department");
            return View(data);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM department)
        {
            await _client.PostAsJsonAsync("Department", department);
            return RedirectToAction("Index");
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var department =
            await _client.GetFromJsonAsync<DepartmentVM>($"Department/{id}");

            return View(department);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentVM department)
        {
            await _client.PutAsJsonAsync($"Department/{department.Id}", department);

            return RedirectToAction("Index");
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync($"Department/{id}");

            return RedirectToAction("Index");
        }
    }
}
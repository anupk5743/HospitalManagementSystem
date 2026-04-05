using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;
using HMS.Web.Models;
using HMS.Web.Filters;

namespace HMS.Web.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class DoctorsController : Controller
    {
        private readonly HttpClient _client;

        public DoctorsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44382/Api/");
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var doctors = await _client.GetFromJsonAsync<List<DoctorVM>>("Doctor");
            return View(doctors);
        }

        // CREATE GET
        public async Task<IActionResult> Create()
        {
            var model = new DoctorVM();
            await LoadDepartments(model);
            return View(model);
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(DoctorVM doctor)
        {
            if (ModelState.IsValid)
            {
                await _client.PostAsJsonAsync("Doctor", doctor);
                return RedirectToAction("Index");
            }

            await LoadDepartments(doctor);
            return View(doctor);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _client.GetFromJsonAsync<DoctorVM>($"Doctor/{id}");

            await LoadDepartments(doctor);

            return View(doctor);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, DoctorVM doctor)
        {
            if (ModelState.IsValid)
            {
                await _client.PutAsJsonAsync($"Doctor/{id}", doctor);
                return RedirectToAction("Index");
            }

            await LoadDepartments(doctor);
            return View(doctor);
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync($"Doctor/{id}");
            return RedirectToAction("Index");
        }

        // LOAD DEPARTMENTS (FIXED)
        private async Task LoadDepartments(DoctorVM model)
        {
            var departments = await _client.GetFromJsonAsync<List<DepartmentVM>>("Department");

            if (departments != null)
            {
                model.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();
            }
            else
            {
                model.Departments = new List<SelectListItem>();
            }
        }
    }
}
using HMS.Web.Filters;
using HMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Web.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class PatientsController : Controller
    {
        private readonly HttpClient _client;

        public PatientsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44382/api/");
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var patients = await _client.GetFromJsonAsync<List<PatientVM>>("patients");
            return View(patients);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(PatientVM patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            await _client.PostAsJsonAsync("patients", patient);
            return RedirectToAction("Index");
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _client.GetFromJsonAsync<PatientVM>($"patients/{id}");

            return View(patient);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(PatientVM patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            var response = await _client.PutAsJsonAsync($"patients/{patient.Id}", patient);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content(error);
            }

            return RedirectToAction("Index");
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync($"patients/{id}");
            return RedirectToAction("Index");
        }
    }
}
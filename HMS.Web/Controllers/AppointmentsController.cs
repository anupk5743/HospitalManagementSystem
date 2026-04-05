using Microsoft.AspNetCore.Mvc;
using HMS.Web.Models;
using System.Net.Http.Json;
using HMS.Web.Filters;

namespace HMS.Web.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class AppointmentsController : Controller
    {
        private readonly HttpClient _client;

        public AppointmentsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44382/Api/");
        }

        // =======================
        // LIST
        // =======================
        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<AppointmentVM>>("Appointments");
            return View(data);
        }

        // =======================
        // CREATE GET
        // =======================
        public async Task<IActionResult> Create()
        {
            var patients = await _client.GetFromJsonAsync<List<PatientVM>>("Patients");
            var doctors = await _client.GetFromJsonAsync<List<DoctorVM>>("Doctor");

            ViewBag.Patients = patients ?? new List<PatientVM>();
            ViewBag.Doctors = doctors ?? new List<DoctorVM>();

            return View();
        }

        // =======================
        // CREATE POST
        // =======================
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentVM appointment)
        {
            if (ModelState.IsValid)
            {
                await _client.PostAsJsonAsync("Appointments", appointment);
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // =======================
        // EDIT GET
        // =======================
        public async Task<IActionResult> Edit(int id)
        {
            var appointment =
                await _client.GetFromJsonAsync<AppointmentVM>($"Appointments/{id}");

            var patients =
                await _client.GetFromJsonAsync<List<PatientVM>>("Patients");

            var doctors =
                await _client.GetFromJsonAsync<List<DoctorVM>>("Doctor");

            ViewBag.Patients = patients ?? new List<PatientVM>();
            ViewBag.Doctors = doctors ?? new List<DoctorVM>();

            return View(appointment);
        }

        // =======================
        // EDIT POST
        // =======================
        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentVM appointment)
        {
            await _client.PutAsJsonAsync($"Appointments/{appointment.Id}", appointment);
            return RedirectToAction("Index");
        }

        // =======================
        // DELETE
        // =======================
        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync($"Appointments/{id}");
            return RedirectToAction("Index");
        }

        // =======================
        // DETAILS
        // =======================
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _client.GetFromJsonAsync<AppointmentVM>($"Appointments/{id}");
            return View(appointment);
        }
    }
}
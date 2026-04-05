using HMS.API.Repositories;
using HMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repo;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(
            IPatientRepository repo,
            ILogger<PatientsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all patients");

                var data = await _repo.GetAll();

                if (data == null || !data.Any())
                {
                    _logger.LogWarning("No patients found");
                    return NotFound("No patients found");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching patients");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation("Fetching patient with ID: {Id}", id);

                var patient = await _repo.GetById(id);

                if (patient == null)
                {
                    _logger.LogWarning("Patient not found: {Id}", id);
                    return NotFound("Patient not found");
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching patient");
                return StatusCode(500, "Internal server error");
            }
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            try
            {
                _logger.LogInformation("Creating patient");

                var data = await _repo.Add(patient);

                return Ok(new
                {
                    message = "Patient created successfully",
                    data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating patient");
                return StatusCode(500, "Internal server error");
            }
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
        {
            try
            {
                _logger.LogInformation("Updating patient {Id}", id);

                if (id != patient.Id)
                    return BadRequest("Patient ID mismatch");

                var data = await _repo.Update(patient);

                return Ok(new
                {
                    message = "Patient updated successfully",
                    data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting patient {Id}", id);

                var result = await _repo.Delete(id);

                if (!result)
                {
                    _logger.LogWarning("Patient not found for delete: {Id}", id);
                    return NotFound();
                }

                return Ok("Patient deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting patient");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
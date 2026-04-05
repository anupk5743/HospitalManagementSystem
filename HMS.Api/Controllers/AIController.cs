using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public AIController(IHttpClientFactory factory, IConfiguration config)
        {
            _client = factory.CreateClient();
            _config = config;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string message)
        {
            var apiKey = _config["AIzaSyAsGut3DFyYVqyIdJHv0fwyiFa1LcRJ9Zs"];

            var request = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = message }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(
                $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={apiKey}",
                content);

            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }
    }
}
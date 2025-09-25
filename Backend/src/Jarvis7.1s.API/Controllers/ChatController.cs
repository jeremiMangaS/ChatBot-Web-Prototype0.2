using Jarvis7s.API.DTOs;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Jarvis7s.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    // Constructor
    public ChatController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult> GenerateContent([FromBody] ChatRequest request)
    {
        var apiKey = _configuration["ApiKeys:GoogleAi"];
        var apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
        var client = _httpClientFactory.CreateClient(); // Create client for make request

        // Request Json Structure
        var googleApiRequest = new
        {
            contents = new[]
            {
                new {
                    parts = new[]
                    {
                        new { text = request.Prompt }
                    }
                }
            }
        };

        var jsonContent = JsonSerializer.Serialize(googleApiRequest);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Add("X-goog-api-key", apiKey); // Header

        var response = await client.PostAsync(apiEndpoint, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return Ok(JsonDocument.Parse(responseBody));
        }
        else
        {
            // If failed
            var errorBody = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, errorBody);
        }
    }
}
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
    public async Task<ActionResult> GenerateContent([FromBody] ChatApiRequest request)
    {
        var apiKey = _configuration["ApiKeys:GoogleAi"];
        var apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
        var client = _httpClientFactory.CreateClient(); // Create client for make request

        var formattedContents = request.History.Select(msg => new
        {
            role = msg.Role == "ai" ? "model" : "user",
            parts = new[] { new { text = msg.Content } }
        }).ToList();

        var googleApiRequest = new
        {
            contents = formattedContents
        };

        var jsonContent = JsonSerializer.Serialize(googleApiRequest);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Request Json Structure
        // var googleApiRequest = new
        // {
        //     contents = new[]
        //     {
        //         new {
        //             parts = new[]
        //             {
        //                 new { text = request.Prompt }
        //             }
        //         }
        //     }
        // };

        // var jsonContent = JsonSerializer.Serialize(googleApiRequest);
        // var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Add("X-goog-api-key", apiKey); // Header

        var response = await client.PostAsync(apiEndpoint, httpContent);

        // Response Messages
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    string? extractedText = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                    var simpleResponse = new { responseText = extractedText };

                    return Ok(simpleResponse); // Clean string response
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to parse the response from LLM");
            }

            // return Ok(JsonDocument.Parse(responseBody)); // For Postman
        }
        else
        {
            // If failed
            var errorBody = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, errorBody);
        }
    }

    // Generate Summirize for Memory
    [HttpPost("summarize")]
    public async Task<IActionResult> SummarizeConversation([FromBody] List<ChatMessage> history)
    {
        if (history == null || !history.Any())
        {
            return BadRequest("History cannot be empty.");
        }

        var apiKey = _configuration["ApiKeys:GoogleAi"];
        var apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
        var client = _httpClientFactory.CreateClient();

        var conversationText = string.Join("\n", history.Select(m => $"{m.Role}: {m.Content}"));
        // Summarize prompt
        var summarizationPrompt = $"Summarize the following conversation concisely into key points. Capture all important facts, names, and user intentions, all the importants information about user especially. The summary will be used as a memory for an ongoing AI conversation. Output only the key points.\n\n---\n\n{conversationText}";
        var googleApiRequest = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = summarizationPrompt } } }
            }
        };

        var jsonContent = JsonSerializer.Serialize(googleApiRequest);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Add("X-goog-api-key", apiKey);
        var response = await client.PostAsync(apiEndpoint, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                string extractedText = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return Ok(new { summary = extractedText });
            }
        }
        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}
using Jarvis7s.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jarvis7s.API.Controllers;

[ApiController]
[Route("api/[controller]")] // Controller URL access
public class UsersController : ControllerBase
{
    [HttpGet] // Endpoint
    public IActionResult getUsers() // Seeder
    {
        var users = new List<User>
        {
            new User { Id = 1, Username = "admin", Email = "admin@gmailExample.com", CreatedAt = DateTime.UtcNow },
            new User { Id = 2, Username = "guest", Email = "guest@gmailExampe.com", CreatedAt = DateTime.UtcNow }
        };

        return Ok(users); 
    }
}
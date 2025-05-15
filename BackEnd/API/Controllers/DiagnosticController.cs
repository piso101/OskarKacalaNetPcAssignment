using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Infrastructure.Data;

namespace BackEnd.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DiagnosticController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/health/db
    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        try
        {
            // Minimalne zapytanie do sprawdzenia połączenia
            var canConnect = await _context.Database.CanConnectAsync();

            if (canConnect)
                return Ok("✅ Connection to the database was successful.");
            else
                return StatusCode(500, "❌ Unable to connect to the database.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ Database error: {ex.Message}");
        }
    }
}

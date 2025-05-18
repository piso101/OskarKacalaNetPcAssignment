using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Infrastructure.Data;
using BackEnd.Shared.Helpers;
using BackEnd.Domain.Entities;

namespace BackEnd.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtGenerator _jwtGenerator;

    public DiagnosticController(ApplicationDbContext context, JwtGenerator jwtGenerator)
    {
        _context = context;
        _jwtGenerator = jwtGenerator;
    }

    // GET: api/diagnostic/db
    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        try
        {
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

    // GET: api/diagnostic/token-test
    [HttpGet("token-test")]
    public IActionResult GenerateFakeToken()
    {
        // Tymczasowy użytkownik testowy (symulacja udanego logowania)
        var fakeUser = new User
        {
            Id = 1,
            Email = "diagnostic@example.com"
        };

        var token = _jwtGenerator.GenerateToken(fakeUser);
        return Ok(new
        {
            message = "✅ Token generated successfully.",
            token
        });
    }
}

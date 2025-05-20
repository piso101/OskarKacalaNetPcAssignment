using Microsoft.AspNetCore.Mvc;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.API.Controllers;

/// <summary>
/// Kontroler odpowiedzialny za logowanie, rejestrację oraz uwierzytelnianie użytkowników.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicjalizuje nową instancję kontrolera autoryzacji.
    /// </summary>
    public AuthController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Loguje użytkownika na podstawie adresu e-mail i hasła.
    /// </summary>
    /// <param name="loginDto">Dane logowania użytkownika.</param>
    /// <returns>Token JWT oraz dane użytkownika, jeśli dane są poprawne.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return Unauthorized("Invalid email or password.");
        }

        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid email or password.");
        }
    }

    /// <summary>
    /// Zwraca dane bieżącego zalogowanego użytkownika (na podstawie tokena JWT).
    /// </summary>
    /// <returns>Id i e-mail użytkownika oraz komunikat potwierdzający ważność tokena.</returns>
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);

        return Ok(new
        {
            Id = userId,
            Email = email,
            Message = "Token is valid."
        });
    }

    /// <summary>
    /// Rejestruje nowego użytkownika w systemie.
    /// </summary>
    /// <param name="registerDto">Dane rejestracyjne użytkownika.</param>
    /// <returns>Komunikat potwierdzający sukces lub błąd walidacji.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
    {
        try
        {
            await _authService.RegisterAsync(registerDto);
            return Ok("Registration successful.");
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }
}
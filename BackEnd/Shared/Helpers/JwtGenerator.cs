using BackEnd.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Shared.Helpers;

/// <summary>
/// Pomocnicza klasa odpowiedzialna za generowanie tokenów JWT dla użytkowników.
/// Wykorzystuje konfigurację aplikacji do pozyskania klucza i parametrów tokena.
/// </summary>
public class JwtGenerator
{
    private readonly IConfiguration _config;

    /// <summary>
    /// Inicjalizuje nową instancję klasy JwtGenerator z dostępem do konfiguracji aplikacji.
    /// </summary>
    /// <param name="config">Interfejs konfiguracji zawierający ustawienia JWT.</param>
    public JwtGenerator(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Generuje token JWT na podstawie danych użytkownika.
    /// </summary>
    /// <param name="user">Użytkownik, dla którego generowany jest token.</param>
    /// <returns>Ciąg znaków reprezentujący zaszyfrowany token JWT.</returns>
    public string GenerateToken(User user)
    {
        // Tworzenie listy roszczeń (claims) zawierających identyfikator i adres e-mail użytkownika
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        // Generowanie klucza symetrycznego z tajnego klucza zawartego w konfiguracji
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config["JwtSettings:SecretKey"]!));

        // Ustawienie metody podpisywania tokena (algorytm HMAC SHA-256)
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Tworzenie obiektu JwtSecurityToken z parametrami określonymi w konfiguracji
        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        // Serializacja tokena do postaci tekstowej (string)
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
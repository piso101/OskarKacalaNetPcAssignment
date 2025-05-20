using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BackEnd.Infrastructure.Configurations;

/// <summary>
/// Statyczna klasa rozszerzająca IServiceCollection o konfigurację uwierzytelniania JWT.
/// </summary>
public static class JwtConfiguration
{
    /// <summary>
    /// Rejestruje i konfiguruje uwierzytelnianie JWT w aplikacji ASP.NET Core.
    /// Parametry takie jak SecretKey, Issuer i Audience są pobierane z konfiguracji.
    /// </summary>
    /// <param name="services">Kolekcja usług aplikacji (DI).</param>
    /// <param name="configuration">Obiekt konfiguracji aplikacji (appsettings.json).</param>
    /// <returns>Zmodyfikowana kolekcja usług z dodanym JWT authentication.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Odczytanie ustawień JWT z konfiguracji aplikacji
        var secretKey = configuration["JwtSettings:SecretKey"];
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];

        // Utworzenie klucza podpisującego na podstawie sekretu
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        // Rejestracja schematu JWT i konfiguracja walidacji tokena
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,                // Sprawdź, czy issuer się zgadza
                    ValidateAudience = true,              // Sprawdź, czy audience się zgadza
                    ValidateLifetime = true,              // Sprawdź, czy token nie wygasł
                    ValidateIssuerSigningKey = true,      // Sprawdź integralność tokena
                    ValidIssuer = issuer,                 // Oczekiwany issuer
                    ValidAudience = audience,             // Oczekiwana audience
                    IssuerSigningKey = signingKey         // Klucz do weryfikacji podpisu
                };
            });

        return services;
    }
}
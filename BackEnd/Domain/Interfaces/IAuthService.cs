using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs serwisu odpowiedzialnego za operacje uwierzytelniania i rejestracji użytkowników.
/// Udostępnia metody logowania oraz tworzenia kont.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Asynchronicznie loguje użytkownika na podstawie podanych danych uwierzytelniających.
    /// </summary>
    /// <param name="dto">Obiekt <see cref="UserLoginDto"/> zawierający dane logowania (email i hasło).</param>
    /// <returns>Obiekt <see cref="AuthResultDto"/> zawierający token JWT i informacje o użytkowniku.</returns>
    Task<AuthResultDto> LoginAsync(UserLoginDto dto);

    /// <summary>
    /// Asynchronicznie rejestruje nowego użytkownika w systemie.
    /// </summary>
    /// <param name="dto">Obiekt <see cref="UserRegisterDto"/> zawierający dane rejestracyjne użytkownika.</param>
    /// <returns>Zadanie reprezentujące zakończenie operacji rejestracji.</returns>
    Task RegisterAsync(UserRegisterDto dto);
}
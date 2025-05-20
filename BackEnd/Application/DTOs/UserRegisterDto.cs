namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO używane podczas rejestracji nowego użytkownika.
/// Zawiera dane wymagane do utworzenia konta: e-mail i hasło z potwierdzeniem.
/// </summary>
public class UserRegisterDto
{
    /// <summary>
    /// Adres e-mail użytkownika, który będzie używany jako login.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Hasło użytkownika. Powinno być odpowiednio silne.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Powtórzone hasło, służące do potwierdzenia poprawności wprowadzenia.
    /// </summary>
    public string ConfirmPassword { get; set; } = null!;
}
namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO zwracane po pomyślnym uwierzytelnieniu użytkownika.
/// Zawiera token JWT, datę jego wygaśnięcia oraz dane użytkownika.
/// </summary>
public class AuthResultDto
{
    /// <summary>
    /// Token JWT używany do autoryzacji kolejnych żądań.
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Data i godzina wygaśnięcia tokena JWT (UTC).
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Podstawowe informacje o zalogowanym użytkowniku.
    /// </summary>
    public UserDto User { get; set; } = null!;
}
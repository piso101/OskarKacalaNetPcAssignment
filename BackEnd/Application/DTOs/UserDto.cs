namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO reprezentujące podstawowe dane użytkownika.
/// Używane głównie w odpowiedziach API, np. po zalogowaniu.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Unikalny identyfikator użytkownika.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Adres e-mail użytkownika.
    /// </summary>
    public string Email { get; set; } = null!;
}
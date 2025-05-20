namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO używane podczas logowania użytkownika.
/// Zawiera dane uwierzytelniające: e-mail oraz hasło.
/// </summary>
public class UserLoginDto
{
    /// <summary>
    /// Adres e-mail użytkownika, wykorzystywany jako login.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Hasło użytkownika w formie jawnej (zostanie zweryfikowane podczas logowania).
    /// </summary>
    public string Password { get; set; } = null!;
}
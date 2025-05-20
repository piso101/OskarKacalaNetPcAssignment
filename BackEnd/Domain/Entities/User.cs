namespace BackEnd.Domain.Entities;

/// <summary>
/// Reprezentuje użytkownika systemu.
/// Zawiera dane logowania oraz powiązane kontakty.
/// </summary>
public class User
{
    /// <summary>
    /// Unikalny identyfikator użytkownika.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Adres e-mail użytkownika wykorzystywany jako login.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Zhashowane hasło użytkownika.
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Lista kontaktów przypisanych do użytkownika.
    /// Relacja 1:N – jeden użytkownik może mieć wiele kontaktów.
    /// </summary>
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}

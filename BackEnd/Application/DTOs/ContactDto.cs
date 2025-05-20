namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO reprezentujące dane kontaktu.
/// Używane do prezentacji kontaktu w aplikacji (np. na liście lub w szczegółach).
/// </summary>
public class ContactDto
{
    /// <summary>
    /// Unikalny identyfikator kontaktu.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Imię kontaktu.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Nazwisko kontaktu.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Adres e-mail kontaktu.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Opcjonalny numer telefonu kontaktu.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Opcjonalna data urodzenia kontaktu.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Nazwa przypisanej kategorii głównej.
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Nazwa przypisanej subkategorii lub niestandardowej (jeśli dotyczy).
    /// </summary>
    public string? Subcategory { get; set; }
}
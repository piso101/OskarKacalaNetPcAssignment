namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO używane do aktualizacji danych kontaktu.
/// Zawiera pełny zestaw edytowalnych właściwości.
/// </summary>
public class UpdateContactDto
{
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
    /// Identyfikator przypisanej kategorii głównej.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Opcjonalny identyfikator przypisanej subkategorii.
    /// </summary>
    public int? SubcategoryId { get; set; }

    /// <summary>
    /// Alternatywna, własna nazwa subkategorii (jeśli nie wybrano z listy).
    /// </summary>
    public string? CustomSubcategory { get; set; }
}
namespace BackEnd.Domain.Entities;

/// <summary>
/// Reprezentuje kontakt należący do użytkownika.
/// Zawiera dane osobowe oraz przypisanie do kategorii i (opcjonalnie) subkategorii.
/// </summary>
public class Contact
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
    /// Identyfikator przypisanej kategorii głównej.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Obiekt kategorii przypisanej do kontaktu.
    /// </summary>
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Identyfikator przypisanej subkategorii (opcjonalny).
    /// </summary>
    public int? SubcategoryId { get; set; }

    /// <summary>
    /// Obiekt subkategorii przypisanej do kontaktu (opcjonalnie).
    /// </summary>
    public Subcategory? Subcategory { get; set; }

    /// <summary>
    /// Własna, ręcznie wpisana nazwa subkategorii (jeśli nie wybrano z listy).
    /// </summary>
    public string? CustomSubcategory { get; set; }

    /// <summary>
    /// Identyfikator użytkownika, właściciela kontaktu.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Obiekt użytkownika, do którego należy kontakt.
    /// </summary>
    public User User { get; set; } = null!;
}

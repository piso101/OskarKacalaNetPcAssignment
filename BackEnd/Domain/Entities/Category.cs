namespace BackEnd.Domain.Entities;

/// <summary>
/// Reprezentuje kategorię główną, do której mogą należeć subkategorie oraz kontakty.
/// </summary>
public class Category
{
    /// <summary>
    /// Unikalny identyfikator kategorii.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nazwa kategorii.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Lista subkategorii przypisanych do tej kategorii.
    /// Relacja 1:N – jedna kategoria może mieć wiele subkategorii.
    /// </summary>
    public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

    /// <summary>
    /// Lista kontaktów przypisanych bezpośrednio do tej kategorii.
    /// Relacja 1:N – jedna kategoria może mieć wiele kontaktów.
    /// </summary>
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
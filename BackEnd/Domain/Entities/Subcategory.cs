namespace BackEnd.Domain.Entities;

/// <summary>
/// Reprezentuje podkategorię przypisaną do konkretnej kategorii.
/// Może być powiązana z wieloma kontaktami.
/// </summary>
public class Subcategory
{
    /// <summary>
    /// Unikalny identyfikator subkategorii.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nazwa subkategorii.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Identyfikator powiązanej kategorii nadrzędnej.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Obiekt kategorii nadrzędnej, do której należy ta subkategoria.
    /// </summary>
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Lista kontaktów powiązanych z tą subkategorią.
    /// </summary>
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO reprezentujące subkategorię.
/// Używane do odczytu danych subkategorii wraz z informacją o kategorii nadrzędnej.
/// </summary>
public class SubcategoryDto
{
    /// <summary>
    /// Unikalny identyfikator subkategorii.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nazwa subkategorii.
    /// </summary>
    public required string  Name { get; set; }

    /// <summary>
    /// Identyfikator kategorii nadrzędnej, do której należy subkategoria.
    /// </summary>
    public int CategoryId { get; set; }
}
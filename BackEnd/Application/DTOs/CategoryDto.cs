namespace BackEnd.Application.DTOs;

/// <summary>
/// DTO reprezentujące kategorię główną.
/// Używane do prezentacji listy kategorii lub ich identyfikacji w formularzach.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Unikalny identyfikator kategorii.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nazwa kategorii.
    /// </summary>
    public required string Name { get; set; }
}
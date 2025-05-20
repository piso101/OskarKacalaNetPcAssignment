using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs serwisu aplikacyjnego odpowiedzialnego za operacje związane z kategoriami.
/// Zwraca dane w postaci DTO do użytku w warstwie prezentacji lub API.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Asynchronicznie pobiera wszystkie kategorie z systemu.
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="CategoryDto"/> reprezentujących wszystkie kategorie.</returns>
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
}

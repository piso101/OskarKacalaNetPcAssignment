using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;
/// <summary>
/// Interfejs serwisu aplikacyjnego odpowiedzialnego za operacje związane z subkategoriami.
/// Zwraca dane w postaci DTO przeznaczonych do warstwy prezentacji lub API.
/// </summary>
public interface ISubcategoryService
{
    /// <summary>
    /// Asynchronicznie pobiera wszystkie dostępne subkategorie.
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="SubcategoryDto"/> reprezentujących wszystkie subkategorie.</returns>
    Task<IEnumerable<SubcategoryDto>> GetAllSubcategoriesAsync();

    /// <summary>
    /// Asynchronicznie pobiera subkategorie należące do konkretnej kategorii.
    /// </summary>
    /// <param name="categoryId">Identyfikator kategorii, dla której mają zostać pobrane subkategorie.</param>
    /// <returns>Kolekcja obiektów <see cref="SubcategoryDto"/> przypisanych do wskazanej kategorii.</returns>
    Task<IEnumerable<SubcategoryDto>> GetSubcategoriesByCategoryIdAsync(int categoryId);
}

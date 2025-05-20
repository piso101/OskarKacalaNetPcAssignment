using BackEnd.Application.DTOs;
using BackEnd.Domain.Interfaces;

namespace BackEnd.Application.Services;

/// <summary>
/// Serwis aplikacyjny odpowiedzialny za obsługę logiki związanej z subkategoriami.
/// Korzysta z repozytorium subkategorii do odczytu danych i zwraca DTO.
/// </summary>
public class SubcategoryService : ISubcategoryService
{
    private readonly ISubcategoryRepository _subcategoryRepository;

    /// <summary>
    /// Inicjalizuje nową instancję serwisu subkategorii z repozytorium jako zależnością.
    /// </summary>
    /// <param name="subcategoryRepository">Repozytorium subkategorii.</param>
    public SubcategoryService(ISubcategoryRepository subcategoryRepository)
    {
        _subcategoryRepository = subcategoryRepository;
    }

    /// <summary>
    /// Pobiera wszystkie subkategorie z bazy danych i mapuje je do DTO.
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="SubcategoryDto"/>.</returns>
    public async Task<IEnumerable<SubcategoryDto>> GetAllSubcategoriesAsync()
    {
        var subcategories = await _subcategoryRepository.GetAllAsync();
        return subcategories.Select(sc => new SubcategoryDto
        {
            Id = sc.Id,
            Name = sc.Name,
            CategoryId = sc.CategoryId
        });
    }

    /// <summary>
    /// Pobiera subkategorie przypisane do wskazanej kategorii i mapuje je do DTO.
    /// </summary>
    /// <param name="categoryId">Identyfikator kategorii nadrzędnej.</param>
    /// <returns>Kolekcja obiektów <see cref="SubcategoryDto"/> przypisanych do danej kategorii.</returns>
    public async Task<IEnumerable<SubcategoryDto>> GetSubcategoriesByCategoryIdAsync(int categoryId)
    {
        var subcategories = await _subcategoryRepository.GetByCategoryIdAsync(categoryId);
        return subcategories.Select(sc => new SubcategoryDto
        {
            Id = sc.Id,
            Name = sc.Name,
            CategoryId = sc.CategoryId
        });
    }
}
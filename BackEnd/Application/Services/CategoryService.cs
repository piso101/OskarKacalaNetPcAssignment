using BackEnd.Domain.Interfaces;
using BackEnd.Application.DTOs;

namespace BackEnd.Application.Services;

/// <summary>
/// Serwis aplikacyjny odpowiedzialny za operacje związane z kategoriami.
/// Korzysta z repozytorium kategorii i mapuje dane do postaci DTO.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Inicjalizuje nową instancję serwisu kategorii z zależnością do repozytorium.
    /// </summary>
    /// <param name="categoryRepository">Repozytorium kategorii.</param>
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Pobiera wszystkie kategorie z repozytorium i mapuje je do obiektów DTO.
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="CategoryDto"/> reprezentujących kategorie.</returns>
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }
}
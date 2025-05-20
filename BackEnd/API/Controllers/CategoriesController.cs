using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackEnd.Domain.Interfaces;

namespace BackEnd.API.Controllers;

/// <summary>
/// Kontroler odpowiedzialny za obsługę żądań dotyczących kategorii i podkategorii.
/// Udostępnia dane publiczne — bez konieczności autoryzacji.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ISubcategoryService _subcategoryService;

    /// <summary>
    /// Inicjalizuje nową instancję kontrolera kategorii.
    /// </summary>
    public CategoriesController(ICategoryService categoryService, ISubcategoryService subcategoryService)
    {
        _categoryService = categoryService;
        _subcategoryService = subcategoryService;
    }

    /// <summary>
    /// Pobiera wszystkie dostępne kategorie.
    /// Endpoint jest publicznie dostępny (brak autoryzacji).
    /// </summary>
    /// <returns>Kolekcja obiektów <c>CategoryDto</c>.</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Pobiera wszystkie subkategorie przypisane do wskazanej kategorii.
    /// Endpoint jest publicznie dostępny (brak autoryzacji).
    /// </summary>
    /// <param name="categoryId">Identyfikator kategorii nadrzędnej.</param>
    /// <returns>Kolekcja obiektów <c>SubcategoryDto</c> przypisanych do kategorii.</returns>
    [HttpGet("{categoryId}/subcategories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubcategories(int categoryId)
    {
        var subcategories = await _subcategoryService.GetSubcategoriesByCategoryIdAsync(categoryId);
        return Ok(subcategories);
    }
}
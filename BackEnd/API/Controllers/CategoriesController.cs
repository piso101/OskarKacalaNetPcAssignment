using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackEnd.Domain.Interfaces;

namespace BackEnd.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ISubcategoryService _subcategoryService;

    public CategoriesController(ICategoryService categoryService, ISubcategoryService subcategoryService)
    {
        _categoryService = categoryService;
        _subcategoryService = subcategoryService;
    }

    // Pobierz wszystkie kategorie — publicznie dostępne
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    // Pobierz podkategorie dla danej kategorii — publicznie dostępne
    [HttpGet("{categoryId}/subcategories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubcategories(int categoryId)
    {
        var subcategories = await _subcategoryService.GetSubcategoriesByCategoryIdAsync(categoryId);
        return Ok(subcategories);
    }
}

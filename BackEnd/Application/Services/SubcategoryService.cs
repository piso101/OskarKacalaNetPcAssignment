using BackEnd.Application.DTOs;
using BackEnd.Domain.Interfaces;

namespace BackEnd.Application.Services;
public class SubcategoryService : ISubcategoryService
{
    private readonly ISubcategoryRepository _subcategoryRepository;

    public SubcategoryService(ISubcategoryRepository subcategoryRepository)
    {
        _subcategoryRepository = subcategoryRepository;
    }

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

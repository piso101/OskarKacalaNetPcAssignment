using BackEnd.Application.DTOs;


public interface ISubcategoryService
{
    Task<IEnumerable<SubcategoryDto>> GetAllSubcategoriesAsync();
    Task<IEnumerable<SubcategoryDto>> GetSubcategoriesByCategoryIdAsync(int categoryId);
}

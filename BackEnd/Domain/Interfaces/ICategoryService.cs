using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
}

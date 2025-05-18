using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

public interface ISubcategoryRepository
{
    Task<Subcategory?> GetByIdAsync(int id);
    Task<IEnumerable<Subcategory>> GetAllAsync();
    Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(int categoryId);
}
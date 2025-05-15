using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

public interface ISubcategoryRepository
{
    Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(int categoryId);
}
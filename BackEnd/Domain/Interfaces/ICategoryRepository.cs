using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}
using BackEnd.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
}

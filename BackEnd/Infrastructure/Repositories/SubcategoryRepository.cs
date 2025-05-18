using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly ApplicationDbContext _context;

    public SubcategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Subcategory?> GetByIdAsync(int id)
    {
        return await _context.Subcategories
            .Include(sc => sc.Category)
            .FirstOrDefaultAsync(sc => sc.Id == id);
    }

    public async Task<IEnumerable<Subcategory>> GetAllAsync()
    {
        return await _context.Subcategories
            .Include(sc => sc.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Subcategories
            .Where(sc => sc.CategoryId == categoryId)
            .ToListAsync();
    }
}

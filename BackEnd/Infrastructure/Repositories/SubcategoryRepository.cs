using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

/// <summary>
/// Repozytorium odpowiedzialne za operacje dostępu do danych encji Subcategory.
/// Umożliwia pobieranie subkategorii z bazy danych oraz filtrowanie po kategorii.
/// </summary>
public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicjalizuje nowe repozytorium subkategorii z dostępem do kontekstu bazy danych.
    /// </summary>
    /// <param name="context">Kontekst bazy danych (ApplicationDbContext).</param>
    public SubcategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronicznie pobiera subkategorię na podstawie jej identyfikatora.
    /// Uwzględnia załadowanie powiązanej kategorii (Include).
    /// </summary>
    /// <param name="id">Identyfikator subkategorii.</param>
    /// <returns>Obiekt Subcategory, jeśli znaleziono; w przeciwnym razie null.</returns>
    public async Task<Subcategory?> GetByIdAsync(int id)
    {
        return await _context.Subcategories
            .Include(sc => sc.Category)
            .FirstOrDefaultAsync(sc => sc.Id == id);
    }

    /// <summary>
    /// Asynchronicznie pobiera wszystkie subkategorie z bazy danych,
    /// wraz z powiązanymi kategoriami.
    /// </summary>
    /// <returns>Kolekcja wszystkich subkategorii.</returns>
    public async Task<IEnumerable<Subcategory>> GetAllAsync()
    {
        return await _context.Subcategories
            .Include(sc => sc.Category)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronicznie pobiera subkategorie przypisane do konkretnej kategorii.
    /// </summary>
    /// <param name="categoryId">Identyfikator kategorii.</param>
    /// <returns>Kolekcja subkategorii przypisanych do danej kategorii.</returns>
    public async Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Subcategories
            .Where(sc => sc.CategoryId == categoryId)
            .ToListAsync();
    }
}
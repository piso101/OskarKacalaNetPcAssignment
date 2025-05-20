using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

/// <summary>
/// Repozytorium odpowiedzialne za operacje dostępu do danych encji Category.
/// Umożliwia pobieranie kategorii wraz z ich podkategoriami.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicjalizuje nowe repozytorium kategorii z dostępem do kontekstu bazy danych.
    /// </summary>
    /// <param name="context">Kontekst aplikacji (ApplicationDbContext).</param>
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronicznie pobiera kategorię na podstawie jej identyfikatora,
    /// łącznie z powiązanymi podkategoriami.
    /// </summary>
    /// <param name="id">Identyfikator kategorii.</param>
    /// <returns>Kategoria wraz z podkategoriami, jeśli istnieje; w przeciwnym razie null.</returns>
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Asynchronicznie pobiera wszystkie kategorie wraz z ich podkategoriami.
    /// </summary>
    /// <returns>Lista wszystkich kategorii z podkategoriami.</returns>
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();
    }
}
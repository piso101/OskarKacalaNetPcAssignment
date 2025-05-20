using BackEnd.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs repozytorium kategorii odpowiedzialny za operacje dostępu do danych kategorii.
/// Umożliwia pobieranie pojedynczych i wszystkich kategorii z bazy danych.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Asynchronicznie pobiera kategorię na podstawie jej identyfikatora.
    /// </summary>
    /// <param name="id">Identyfikator kategorii.</param>
    /// <returns>Obiekt <see cref="Category"/>, jeśli istnieje; w przeciwnym razie null.</returns>
    Task<Category?> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronicznie pobiera wszystkie dostępne kategorie.
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="Category"/> reprezentujących wszystkie kategorie w systemie.</returns>
    Task<IEnumerable<Category>> GetAllAsync();
}
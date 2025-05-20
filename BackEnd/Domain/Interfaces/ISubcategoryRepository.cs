using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs repozytorium subkategorii, definiujący operacje dostępu do danych subkategorii.
/// Umożliwia pobieranie subkategorii na różne sposoby.
/// </summary>
public interface ISubcategoryRepository
{
    /// <summary>
    /// Asynchronicznie pobiera subkategorię na podstawie jej identyfikatora.
    /// </summary>
    /// <param name="id">Identyfikator subkategorii.</param>
    /// <returns>Obiekt <see cref="Subcategory"/> jeśli znaleziono; w przeciwnym razie null.</returns>
    Task<Subcategory?> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronicznie pobiera wszystkie subkategorie dostępne w systemie.
    /// </summary>
    /// <returns>Kolekcja wszystkich subkategorii.</returns>
    Task<IEnumerable<Subcategory>> GetAllAsync();

    /// <summary>
    /// Asynchronicznie pobiera subkategorie przypisane do danej kategorii.
    /// </summary>
    /// <param name="categoryId">Identyfikator kategorii nadrzędnej.</param>
    /// <returns>Kolekcja subkategorii należących do podanej kategorii.</returns>
    Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(int categoryId);
}
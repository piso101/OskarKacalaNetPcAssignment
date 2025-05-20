using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs serwisu aplikacyjnego odpowiedzialnego za operacje biznesowe na kontaktach.
/// Udostępnia metody CRUD oraz filtrowanie po użytkowniku.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Pobiera wszystkie kontakty z systemu (bez filtrowania po użytkowniku).
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="ContactDto"/>.</returns>
    Task<IEnumerable<ContactDto>> GetAllAsync();

    /// <summary>
    /// Pobiera kontakt na podstawie jego identyfikatora.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <returns>Obiekt <see cref="ContactDto"/>, jeśli istnieje; w przeciwnym razie null.</returns>
    Task<ContactDto?> GetByIdAsync(int id);

    /// <summary>
    /// Pobiera wszystkie kontakty należące do konkretnego użytkownika.
    /// </summary>
    /// <param name="userId">Identyfikator użytkownika.</param>
    /// <returns>Kolekcja obiektów <see cref="ContactDto"/> należących do użytkownika.</returns>
    Task<IEnumerable<ContactDto>> GetAllForUserAsync(int userId);

    /// <summary>
    /// Pobiera kontakt użytkownika na podstawie identyfikatora kontaktu i użytkownika.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <param name="userId">Identyfikator użytkownika.</param>
    /// <returns>Obiekt <see cref="ContactDto"/> jeśli istnieje i należy do użytkownika; w przeciwnym razie null.</returns>
    Task<ContactDto?> GetByIdForUserAsync(int id, int userId);

    /// <summary>
    /// Tworzy nowy kontakt na podstawie danych wejściowych i przypisuje go do użytkownika.
    /// </summary>
    /// <param name="dto">Dane kontaktu do utworzenia.</param>
    /// <param name="userId">Identyfikator użytkownika, właściciela kontaktu.</param>
    /// <returns>Utworzony kontakt jako <see cref="ContactDto"/>.</returns>
    Task<ContactDto> CreateAsync(CreateContactDto dto, int userId);

    /// <summary>
    /// Aktualizuje kontakt użytkownika.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <param name="dto">Dane do aktualizacji.</param>
    /// <param name="userId">Identyfikator właściciela kontaktu.</param>
    /// <returns>true, jeśli aktualizacja się powiodła; w przeciwnym razie false.</returns>
    Task<bool> UpdateAsync(int id, UpdateContactDto dto, int userId);

    /// <summary>
    /// Usuwa kontakt użytkownika.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <param name="userId">Identyfikator właściciela kontaktu.</param>
    /// <returns>true, jeśli usunięcie się powiodło; w przeciwnym razie false.</returns>
    Task<bool> DeleteAsync(int id, int userId);
}
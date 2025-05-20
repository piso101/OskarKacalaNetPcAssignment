using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs repozytorium kontaktów definiujący operacje dostępu do danych kontaktów.
/// Oddziela logikę infrastrukturalną od warstwy domenowej.
/// </summary>
public interface IContactRepository
{
    /// <summary>
    /// Asynchronicznie dodaje kontakt do bazy danych.
    /// </summary>
    /// <param name="contact">Obiekt kontaktu do dodania.</param>
    Task AddAsync(Contact contact);

    /// <summary>
    /// Asynchronicznie aktualizuje kontakt w bazie danych.
    /// </summary>
    /// <param name="contact">Zaktualizowany kontakt.</param>
    Task UpdateAsync(Contact contact);

    /// <summary>
    /// Asynchronicznie usuwa kontakt z bazy danych.
    /// </summary>
    /// <param name="contact">Kontakt do usunięcia.</param>
    Task DeleteAsync(Contact contact);

    /// <summary>
    /// Pobiera wszystkie kontakty użytkownika na podstawie jego identyfikatora.
    /// </summary>
    /// <param name="userId">Identyfikator użytkownika.</param>
    /// <returns>Kolekcja kontaktów.</returns>
    Task<IEnumerable<Contact>> GetAllForUserAsync(int userId);

    /// <summary>
    /// Pobiera kontakt przypisany do konkretnego użytkownika.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <param name="userId">Identyfikator właściciela kontaktu.</param>
    /// <returns>Kontakt, jeśli istnieje i należy do użytkownika; w przeciwnym razie null.</returns>
    Task<Contact?> GetByIdForUserAsync(int id, int userId);

    /// <summary>
    /// Pobiera wszystkie kontakty bez filtrowania po użytkowniku.
    /// </summary>
    /// <returns>Kolekcja wszystkich kontaktów.</returns>
    Task<IEnumerable<Contact>> GetAllAsync();

    /// <summary>
    /// Pobiera kontakt na podstawie jego identyfikatora.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <returns>Kontakt, jeśli istnieje; w przeciwnym razie null.</returns>
    Task<Contact?> GetByIdAsync(int id);

    /// <summary>
    /// Sprawdza, czy istnieje kontakt o podanym adresie e-mail.
    /// </summary>
    /// <param name="email">Adres e-mail kontaktu.</param>
    /// <returns>true, jeśli istnieje kontakt z podanym e-mailem; false w przeciwnym razie.</returns>
    Task<bool> ExistsByEmailAsync(string email);
}
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

/// <summary>
/// Repozytorium implementujące operacje CRUD oraz zapytania domenowe dla encji Contact.
/// Odpowiada za interakcję z bazą danych przy użyciu EF Core.
/// </summary>
public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicjalizuje nowe repozytorium kontaktów z dostępem do kontekstu bazy danych.
    /// </summary>
    /// <param name="context">Kontekst aplikacji (ApplicationDbContext).</param>
    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Pobiera wszystkie kontakty powiązane z danym użytkownikiem.
    /// Uwzględnia załadowanie kategorii i subkategorii.
    /// </summary>
    /// <param name="userId">Identyfikator użytkownika.</param>
    /// <returns>Kolekcja kontaktów należących do użytkownika.</returns>
    public async Task<IEnumerable<Contact>> GetAllForUserAsync(int userId)
    {
        return await _context.Contacts
            .Where(c => c.UserId == userId)
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .ToListAsync();
    }

    /// <summary>
    /// Pobiera pojedynczy kontakt na podstawie identyfikatora i użytkownika.
    /// Uwzględnia relacje z kategorią i subkategorią.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <param name="userId">Identyfikator właściciela kontaktu.</param>
    /// <returns>Kontakt, jeśli istnieje i należy do użytkownika; w przeciwnym razie null.</returns>
    public async Task<Contact?> GetByIdForUserAsync(int id, int userId)
    {
        return await _context.Contacts
            .Where(c => c.Id == id && c.UserId == userId)
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Dodaje nowy kontakt do bazy danych i zapisuje zmiany.
    /// </summary>
    /// <param name="contact">Obiekt kontaktu do dodania.</param>
    public async Task AddAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Aktualizuje istniejący kontakt w bazie danych i zapisuje zmiany.
    /// </summary>
    /// <param name="contact">Zaktualizowany obiekt kontaktu.</param>
    public async Task UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Usuwa kontakt z bazy danych i zapisuje zmiany.
    /// </summary>
    /// <param name="contact">Kontakt do usunięcia.</param>
    public async Task DeleteAsync(Contact contact)
    {
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Sprawdza, czy istnieje kontakt z podanym adresem e-mail.
    /// </summary>
    /// <param name="email">Adres e-mail kontaktu.</param>
    /// <returns>true jeśli istnieje kontakt z danym e-mailem; w przeciwnym razie false.</returns>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Contacts.AnyAsync(c => c.Email == email);
    }

    /// <summary>
    /// Pobiera wszystkie kontakty (bez filtrowania po użytkowniku).
    /// Uwzględnia powiązane kategorie i subkategorie.
    /// </summary>
    /// <returns>Lista wszystkich kontaktów w systemie.</returns>
    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _context.Contacts
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .ToListAsync();
    }

    /// <summary>
    /// Pobiera pojedynczy kontakt na podstawie jego identyfikatora.
    /// Uwzględnia załadowanie powiązanych danych.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <returns>Kontakt, jeśli istnieje; w przeciwnym razie null.</returns>
    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

/// <summary>
/// Repozytorium implementujące operacje dostępu do danych dla encji User.
/// Odpowiada za komunikację z bazą danych poprzez kontekst EF Core.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicjalizuje nową instancję repozytorium użytkownika z dostępem do kontekstu bazy danych.
    /// </summary>
    /// <param name="context">Instancja ApplicationDbContext wstrzyknięta przez kontener DI.</param>
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronicznie pobiera użytkownika na podstawie adresu e-mail.
    /// </summary>
    /// <param name="email">Adres e-mail użytkownika.</param>
    /// <returns>Obiekt User, jeśli znaleziono; w przeciwnym razie null.</returns>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Asynchronicznie dodaje nowego użytkownika do bazy danych i zapisuje zmiany.
    /// </summary>
    /// <param name="user">Obiekt użytkownika do dodania.</param>
    /// <returns>Zadanie reprezentujące operację zapisu.</returns>
    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}
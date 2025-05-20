using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

/// <summary>
/// Interfejs repozytorium użytkownika definiujący operacje dostępu do danych.
/// Umożliwia pobieranie i dodawanie użytkowników w warstwie domenowej.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Asynchronicznie pobiera użytkownika na podstawie jego adresu e-mail.
    /// </summary>
    /// <param name="email">Adres e-mail użytkownika do wyszukania.</param>
    /// <returns>Obiekt <see cref="User"/> jeśli istnieje; w przeciwnym razie null.</returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Asynchronicznie dodaje nowego użytkownika do źródła danych.
    /// </summary>
    /// <param name="user">Obiekt użytkownika do dodania.</param>
    /// <returns>Zadanie reprezentujące operację zapisu.</returns>
    Task AddAsync(User user);
}
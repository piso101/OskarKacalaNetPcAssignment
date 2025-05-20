using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infrastructure.Data.Configurations;

/// <summary>
/// Konfiguracja encji User dla Entity Framework Core.
/// Określa właściwości, indeksy i relacje między użytkownikiem a jego kontaktami.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Konfiguruje mapowanie encji User do struktury bazy danych.
    /// </summary>
    /// <param name="builder">Obiekt konfiguracyjny dla encji User.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Klucz główny
        builder.HasKey(u => u.Id);

        // Wymagany adres e-mail z ograniczeniem długości
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        // Unikalny indeks dla adresu e-mail
        builder.HasIndex(u => u.Email)
            .IsUnique();

        // Hash hasła użytkownika (pole wymagane)
        builder.Property(u => u.PasswordHash)
            .IsRequired();

        // Relacja: User → Contacts (użytkownik może mieć wiele kontaktów)
        // Usunięcie użytkownika powoduje kaskadowe usunięcie jego kontaktów
        builder.HasMany(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

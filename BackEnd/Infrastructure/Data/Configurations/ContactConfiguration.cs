using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infrastructure.Data.Configurations;

/// <summary>
/// Konfiguracja encji Contact dla Entity Framework Core.
/// Definiuje indeksy, ograniczenia długości, relacje oraz zachowania przy usuwaniu.
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <summary>
    /// Mapuje właściwości encji Contact do kolumn bazy danych i konfiguruje relacje.
    /// </summary>
    /// <param name="builder">Obiekt służący do konfigurowania encji.</param>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        // Indeks unikalny dla pola Email (gwarantuje unikalność adresu e-mail kontaktu)
        builder.HasIndex(c => c.Email).IsUnique();

        // Wymagane dane kontaktowe z ograniczeniami długości
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        // Relacja: Contact → User (właściciel kontaktu)
        // Usunięcie użytkownika powoduje usunięcie jego kontaktów (Cascade)
        builder.HasOne(c => c.User)
               .WithMany(u => u.Contacts)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relacja: Contact → Category
        // Usunięcie kategorii ustawia wartość klucza obcego na NULL
        builder.HasOne(c => c.Category)
               .WithMany(cat => cat.Contacts)
               .HasForeignKey(c => c.CategoryId)
               .OnDelete(DeleteBehavior.SetNull);

        // Relacja: Contact → Subcategory
        // Usunięcie subkategorii ustawia wartość klucza obcego na NULL
        builder.HasOne(c => c.Subcategory)
               .WithMany(sub => sub.Contacts)
               .HasForeignKey(c => c.SubcategoryId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

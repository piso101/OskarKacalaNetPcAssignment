using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infrastructure.Data.Configurations;

/// <summary>
/// Konfiguracja encji Category dla Entity Framework Core.
/// Definiuje klucze główne, ograniczenia długości, relacje oraz zachowania usuwania.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Metoda konfigurująca mapowanie właściwości encji Category do kolumn bazy danych.
    /// </summary>
    /// <param name="builder">Obiekt do konfigurowania encji.</param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Ustawienie klucza głównego
        builder.HasKey(c => c.Id);

        // Wymagane pole 'Name' z maksymalną długością 50 znaków
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        // Relacja 1:N: Category → Subcategories
        // Przy usunięciu kategorii — podkategorie również zostaną usunięte (Cascade)
        builder.HasMany(c => c.Subcategories)
            .WithOne(sc => sc.Category)
            .HasForeignKey(sc => sc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacja 1:N: Category → Contacts
        // Przy usunięciu kategorii — brak określonego zachowania (domyślne: Restrict lub ClientSetNull)
        builder.HasMany(c => c.Contacts)
            .WithOne(contact => contact.Category)
            .HasForeignKey(contact => contact.CategoryId);
    }
}

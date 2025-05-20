using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infrastructure.Data.Configurations;

/// <summary>
/// Konfiguracja encji Subcategory dla Entity Framework Core.
/// Definiuje klucz główny, ograniczenia długości, relacje oraz zachowanie przy usuwaniu.
/// </summary>
public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
{
    /// <summary>
    /// Mapuje właściwości encji Subcategory do kolumn bazy danych i definiuje relacje z innymi encjami.
    /// </summary>
    /// <param name="builder">Obiekt konfigurujący encję.</param>
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        // Ustawienie klucza głównego
        builder.HasKey(s => s.Id);

        // Wymagana nazwa subkategorii z maksymalną długością 50 znaków
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        // Relacja: Subcategory → Category (każda subkategoria należy do jednej kategorii)
        builder.HasOne(s => s.Category)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(s => s.CategoryId);

        // Relacja: Subcategory → Contacts (opcjonalna relacja z kontaktami)
        // Usunięcie subkategorii ustawia klucz obcy w kontaktach na NULL
        builder.HasMany(s => s.Contacts)
            .WithOne(c => c.Subcategory)
            .HasForeignKey(c => c.SubcategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

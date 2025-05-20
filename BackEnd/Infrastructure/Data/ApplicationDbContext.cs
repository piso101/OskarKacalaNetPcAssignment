using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Data;

/// <summary>
/// Główny kontekst bazy danych używany przez Entity Framework Core.
/// Zawiera konfigurację encji oraz mechanizm wstrzykiwania zależności DbContextOptions.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Konstruktor kontekstu przyjmujący opcje konfiguracyjne z DI.
    /// </summary>
    /// <param name="options">Opcje konfiguracyjne dostarczane przez kontener DI.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    /// <summary>
    /// Reprezentuje tabelę użytkowników.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Reprezentuje tabelę kontaktów.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Reprezentuje główne kategorie.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Reprezentuje podkategorie (relacja z Category).
    /// </summary>
    public DbSet<Subcategory> Subcategories { get; set; }

    /// <summary>
    /// Konfiguruje model EF Core przy starcie aplikacji.
    /// </summary>
    /// <param name="modelBuilder">Obiekt budujący model encji.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatyczne zastosowanie wszystkich konfiguracji IEntityTypeConfiguration<T> z bieżącego assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

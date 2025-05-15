using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infrastructure.Data.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);

        builder.HasOne(c => c.User)
               .WithMany(u => u.Contacts)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Category)
               .WithMany(cat => cat.Contacts)
               .HasForeignKey(c => c.CategoryId);

        builder.HasOne(c => c.Subcategory)
               .WithMany(sub => sub.Contacts)
               .HasForeignKey(c => c.SubcategoryId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

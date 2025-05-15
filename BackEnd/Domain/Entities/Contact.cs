namespace BackEnd.Domain.Entities;
public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int? SubcategoryId { get; set; }
    public Subcategory? Subcategory { get; set; }
    public string? CustomSubcategory { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

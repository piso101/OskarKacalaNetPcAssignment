namespace BackEnd.Application.DTOs;

public class ContactDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Category { get; set; } = null!;
    public string? Subcategory { get; set; }
}

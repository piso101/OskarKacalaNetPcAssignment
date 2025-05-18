namespace BackEnd.Application.DTOs;

public class CreateContactDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public string? CustomSubcategory { get; set; }
    public int UserId { get; set; }
}

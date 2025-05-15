namespace BackEnd.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
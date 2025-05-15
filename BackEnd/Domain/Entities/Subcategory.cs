namespace BackEnd.Domain.Entities;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}

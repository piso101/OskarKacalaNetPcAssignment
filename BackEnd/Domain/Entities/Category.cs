namespace BackEnd.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}

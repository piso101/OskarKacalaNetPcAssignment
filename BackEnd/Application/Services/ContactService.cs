using BackEnd.Application.DTOs;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;


namespace Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;

    public ContactService(
        IContactRepository contactRepository,
        ICategoryRepository categoryRepository,
        ISubcategoryRepository subcategoryRepository)
    {
        _contactRepository = contactRepository;
        _categoryRepository = categoryRepository;
        _subcategoryRepository = subcategoryRepository;
    }

    public async Task<IEnumerable<ContactDto>> GetAllAsync()
    {
        var contacts = await _contactRepository.GetAllAsync();
        return contacts.Select(c => new ContactDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Category = c.Category.Name,
            Subcategory = c.Subcategory?.Name ?? c.CustomSubcategory,
            DateOfBirth = c.DateOfBirth
        });
    }

    public async Task<ContactDto?> GetByIdAsync(int id)
    {
        var c = await _contactRepository.GetByIdAsync(id);
        if (c == null) return null;

        return new ContactDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Category = c.Category.Name,
            Subcategory = c.Subcategory?.Name ?? c.CustomSubcategory,
            DateOfBirth = c.DateOfBirth
        };
    }

    public async Task<int> CreateAsync(CreateContactDto dto)
    {
        var contact = new Contact
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            DateOfBirth = dto.DateOfBirth,
            CategoryId = dto.CategoryId,
            SubcategoryId = dto.SubcategoryId,
            CustomSubcategory = dto.CustomSubcategory,
            UserId = dto.UserId // zakładamy, że userId jest przekazany lub znany z JWT
        };

        await _contactRepository.AddAsync(contact);
        return contact.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdateContactDto dto)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) return false;

        contact.FirstName = dto.FirstName;
        contact.LastName = dto.LastName;
        contact.Email = dto.Email;
        contact.PhoneNumber = dto.PhoneNumber;
        contact.DateOfBirth = dto.DateOfBirth;
        contact.CategoryId = dto.CategoryId;
        contact.SubcategoryId = dto.SubcategoryId;
        contact.CustomSubcategory = dto.CustomSubcategory;

        await _contactRepository.UpdateAsync(contact);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) return false;

        await _contactRepository.DeleteAsync(contact);
        return true;
    }
}

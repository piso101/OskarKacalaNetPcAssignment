using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;

namespace BackEnd.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;

    public ContactService(
        IContactRepository contactRepository,
        ICategoryRepository categoryRepository,
        ISubcategoryRepository subcategoryRepository,
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _categoryRepository = categoryRepository;
        _subcategoryRepository = subcategoryRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ContactDto>> GetAllAsync()
    {
        var contacts = await _contactRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }

    public async Task<ContactDto?> GetByIdAsync(int id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) return null;
        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<IEnumerable<ContactDto>> GetAllForUserAsync(int userId)
    {
        var contacts = await _contactRepository.GetAllForUserAsync(userId);
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }

    public async Task<ContactDto?> GetByIdForUserAsync(int id, int userId)
    {
        var contact = await _contactRepository.GetByIdForUserAsync(id, userId);
        if (contact == null) return null;

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> CreateAsync(CreateContactDto dto, int userId)
    {
        var contact = _mapper.Map<Contact>(dto);
        contact.UserId = userId;

        if (contact.DateOfBirth.HasValue) contact.DateOfBirth = DateTime.SpecifyKind(contact.DateOfBirth.Value, DateTimeKind.Utc);

        await _contactRepository.AddAsync(contact);

        // Załaduj Category i Subcategory by mieć nazwy w DTO (opcjonalne)
        contact.Category = await _categoryRepository.GetByIdAsync(contact.CategoryId);
        contact.Subcategory = contact.SubcategoryId != null
            ? await _subcategoryRepository.GetByIdAsync(contact.SubcategoryId.Value)
            : null;

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<bool> UpdateAsync(int id, UpdateContactDto dto, int userId)
    {
        var contact = await _contactRepository.GetByIdForUserAsync(id, userId);
        if (contact == null) return false;

        _mapper.Map(dto, contact);

        // Konwersja daty na UTC
        if (contact.DateOfBirth.HasValue)
        {
            contact.DateOfBirth = DateTime.SpecifyKind(contact.DateOfBirth.Value, DateTimeKind.Utc);
        }

        await _contactRepository.UpdateAsync(contact);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var contact = await _contactRepository.GetByIdForUserAsync(id, userId);
        if (contact == null) return false;

        await _contactRepository.DeleteAsync(contact);
        return true;
    }
}

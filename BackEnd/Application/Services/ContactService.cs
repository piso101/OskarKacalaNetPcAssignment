using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;

namespace BackEnd.Application.Services;

/// <summary>
/// Serwis aplikacyjny odpowiedzialny za operacje biznesowe na kontaktach.
/// Obsługuje tworzenie, pobieranie, edytowanie i usuwanie kontaktów.
/// </summary>
public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicjalizuje nową instancję serwisu kontaktów z wymaganymi zależnościami.
    /// </summary>
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

    /// <summary>
    /// Pobiera wszystkie kontakty z systemu (bez filtrowania po użytkowniku).
    /// </summary>
    public async Task<IEnumerable<ContactDto>> GetAllAsync()
    {
        var contacts = await _contactRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }

    /// <summary>
    /// Pobiera kontakt na podstawie jego identyfikatora.
    /// </summary>
    public async Task<ContactDto?> GetByIdAsync(int id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) return null;
        return _mapper.Map<ContactDto>(contact);
    }

    /// <summary>
    /// Pobiera wszystkie kontakty przypisane do danego użytkownika.
    /// </summary>
    public async Task<IEnumerable<ContactDto>> GetAllForUserAsync(int userId)
    {
        var contacts = await _contactRepository.GetAllForUserAsync(userId);
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }

    /// <summary>
    /// Pobiera konkretny kontakt użytkownika na podstawie jego ID.
    /// </summary>
    public async Task<ContactDto?> GetByIdForUserAsync(int id, int userId)
    {
        var contact = await _contactRepository.GetByIdForUserAsync(id, userId);
        if (contact == null) return null;
        return _mapper.Map<ContactDto>(contact);
    }

    /// <summary>
    /// Tworzy nowy kontakt przypisany do konkretnego użytkownika.
    /// </summary>
    public async Task<ContactDto> CreateAsync(CreateContactDto dto, int userId)
    {
        var contact = _mapper.Map<Contact>(dto);
        contact.UserId = userId;

        // Wymuszenie UTC przy zapisie daty urodzenia
        if (contact.DateOfBirth.HasValue)
            contact.DateOfBirth = DateTime.SpecifyKind(contact.DateOfBirth.Value, DateTimeKind.Utc);

        await _contactRepository.AddAsync(contact);

        // Załaduj kategorię i subkategorię, jeśli istnieją – wymagane do pełnego DTO
        var category = await _categoryRepository.GetByIdAsync(contact.CategoryId);
        if (category == null)
            throw new InvalidOperationException($"Category with ID {contact.CategoryId} not found.");
        contact.Subcategory = contact.SubcategoryId != null
            ? await _subcategoryRepository.GetByIdAsync(contact.SubcategoryId.Value)
            : null;

        return _mapper.Map<ContactDto>(contact);
    }

    /// <summary>
    /// Aktualizuje istniejący kontakt użytkownika.
    /// </summary>
    public async Task<bool> UpdateAsync(int id, UpdateContactDto dto, int userId)
    {
        var contact = await _contactRepository.GetByIdForUserAsync(id, userId);
        if (contact == null) return false;

        _mapper.Map(dto, contact);

        // Wymuszenie UTC przy zapisie daty urodzenia
        if (contact.DateOfBirth.HasValue)
            contact.DateOfBirth = DateTime.SpecifyKind(contact.DateOfBirth.Value, DateTimeKind.Utc);

        await _contactRepository.UpdateAsync(contact);
        return true;
    }

    /// <summary>
    /// Usuwa kontakt użytkownika na podstawie ID i właściciela.
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var contact = await _contactRepository.GetByIdForUserAsync(id, userId);
        if (contact == null) return false;

        await _contactRepository.DeleteAsync(contact);
        return true;
    }
}
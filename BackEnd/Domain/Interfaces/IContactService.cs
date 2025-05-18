using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactDto>> GetAllAsync();  // publiczne - wszystkie kontakty
    Task<ContactDto?> GetByIdAsync(int id);
    public Task<IEnumerable<ContactDto>> GetAllForUserAsync(int userId);
    public Task<ContactDto?> GetByIdForUserAsync(int id, int userId);
    Task<ContactDto> CreateAsync(CreateContactDto dto, int userId);
    Task<bool> UpdateAsync(int id, UpdateContactDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}

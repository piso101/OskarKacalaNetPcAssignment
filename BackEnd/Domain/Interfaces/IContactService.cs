using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactDto>> GetAllAsync();
    Task<ContactDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateContactDto dto);
    Task<bool> UpdateAsync(int id, UpdateContactDto dto);
    Task<bool> DeleteAsync(int id);
}

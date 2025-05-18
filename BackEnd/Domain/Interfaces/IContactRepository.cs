using BackEnd.Application.DTOs;
using BackEnd.Domain.Entities;

namespace BackEnd.Domain.Interfaces;

public interface IContactRepository
{
    Task AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(Contact contact);
    Task<IEnumerable<Contact>> GetAllForUserAsync(int userId);
    Task<Contact?> GetByIdForUserAsync(int id, int userId);
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task<bool> ExistsByEmailAsync(string email);

}
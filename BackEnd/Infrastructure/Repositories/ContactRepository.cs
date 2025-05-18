using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Contact>> GetAllForUserAsync(int userId)
    {
        return await _context.Contacts
            .Where(c => c.UserId == userId)
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .ToListAsync();
    }

    public async Task<Contact?> GetByIdForUserAsync(int id, int userId)
    {
        return await _context.Contacts
            .Where(c => c.Id == id && c.UserId == userId)
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Contacts.AnyAsync(c => c.Email == email);
    }
    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _context.Contacts
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts
            .Include(c => c.Category)
            .Include(c => c.Subcategory)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}

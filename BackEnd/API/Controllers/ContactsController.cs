using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Interfaces;
using System.Security.Claims;

namespace BackEnd.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // Publicznie dostępne - bez autoryzacji
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _contactService.GetAllAsync(); // wszystkie kontakty
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var contact = await _contactService.GetByIdAsync(id); // publicznie dostępne
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    // Poniższe metody tylko dla zalogowanych (autoryzacja)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
    {
        int userId = GetUserId();
        var createdContact = await _contactService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContactDto dto)
    {
        int userId = GetUserId();
        bool updated = await _contactService.UpdateAsync(id, dto, userId);
        if (!updated) return NotFound();

        var updatedContact = await _contactService.GetByIdForUserAsync(id, userId);
        return Ok(updatedContact);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        int userId = GetUserId();
        bool deleted = await _contactService.DeleteAsync(id, userId);
        if (!deleted) return NotFound();
        return NoContent();
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("User ID claim missing");
        return int.Parse(userIdClaim);
    }
}

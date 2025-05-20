using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Interfaces;
using System.Security.Claims;

namespace BackEnd.API.Controllers;

/// <summary>
/// Kontroler odpowiedzialny za operacje na kontaktach.
/// Obsługuje zarówno zapytania publiczne, jak i działania użytkowników zalogowanych.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    /// <summary>
    /// Inicjalizuje nową instancję kontrolera kontaktów.
    /// </summary>
    /// <param name="contactService">Serwis do obsługi logiki kontaktów.</param>
    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Pobiera wszystkie kontakty dostępne publicznie.
    /// </summary>
    /// <returns>Lista obiektów <see cref="ContactDto"/>.</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _contactService.GetAllAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Pobiera konkretny kontakt na podstawie jego identyfikatora.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <returns>Obiekt <see cref="ContactDto"/> jeśli istnieje; w przeciwnym razie 404.</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var contact = await _contactService.GetByIdAsync(id);
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    /// <summary>
    /// Tworzy nowy kontakt powiązany z zalogowanym użytkownikiem.
    /// </summary>
    /// <param name="dto">Dane kontaktu do utworzenia.</param>
    /// <returns>Utworzony kontakt oraz lokalizacja zasobu.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
    {
        int userId = GetUserId();
        var createdContact = await _contactService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
    }

    /// <summary>
    /// Aktualizuje istniejący kontakt należący do zalogowanego użytkownika.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu.</param>
    /// <param name="dto">Dane do aktualizacji.</param>
    /// <returns>Zaktualizowany kontakt lub 404, jeśli nie znaleziono.</returns>
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

    /// <summary>
    /// Usuwa kontakt użytkownika.
    /// </summary>
    /// <param name="id">Identyfikator kontaktu do usunięcia.</param>
    /// <returns>Kod 204, jeśli usunięto; 404, jeśli nie znaleziono.</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        int userId = GetUserId();
        bool deleted = await _contactService.DeleteAsync(id, userId);
        if (!deleted) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Pobiera identyfikator aktualnie zalogowanego użytkownika z tokena JWT.
    /// </summary>
    /// <returns>Id użytkownika jako <c>int</c>.</returns>
    /// <exception cref="UnauthorizedAccessException">Rzucany, gdy brakuje identyfikatora w tokenie.</exception>
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("User ID claim missing");

        return int.Parse(userIdClaim);
    }
}
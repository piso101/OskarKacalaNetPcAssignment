namespace BackEnd.Application.DTOs;

public class AuthResultDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}

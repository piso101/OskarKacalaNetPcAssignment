using BackEnd.Application.DTOs;

namespace BackEnd.Domain.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> LoginAsync(UserLoginDto dto);
    Task RegisterAsync(UserRegisterDto dto);

}

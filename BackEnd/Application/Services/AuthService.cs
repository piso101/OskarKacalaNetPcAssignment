using BackEnd.Application.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using BackEnd.Shared.Helpers;
using BCrypt.Net;


namespace BackEnd.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtGenerator _jwtGenerator;

        public AuthService(IUserRepository userRepository, JwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResultDto> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var token = _jwtGenerator.GenerateToken(user);

            return new AuthResultDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email
                }
            };
        }
    }
}

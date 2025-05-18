using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Shared.Helpers;
using BCrypt.Net;

namespace BackEnd.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, JwtGenerator jwtGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
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
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("User with this email already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = passwordHash;

        await _userRepository.AddAsync(user);
    }
}

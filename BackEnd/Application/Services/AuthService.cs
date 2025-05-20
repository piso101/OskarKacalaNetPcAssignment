using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Interfaces;
using BackEnd.Shared.Helpers;

namespace BackEnd.Application.Services;

/// <summary>
/// Serwis odpowiedzialny za logikę autoryzacji i rejestracji użytkowników.
/// Używa hashowania haseł i generowania tokenów JWT.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicjalizuje nową instancję serwisu autoryzacyjnego z wymaganymi zależnościami.
    /// </summary>
    /// <param name="userRepository">Repozytorium użytkowników.</param>
    /// <param name="jwtGenerator">Generator tokenów JWT.</param>
    /// <param name="mapper">Mapper do konwersji DTO ↔ encje.</param>
    public AuthService(IUserRepository userRepository, JwtGenerator jwtGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
    }

    /// <summary>
    /// Loguje użytkownika na podstawie adresu e-mail i hasła.
    /// W przypadku powodzenia zwraca token JWT oraz dane użytkownika.
    /// </summary>
    /// <param name="dto">Dane logowania użytkownika (email i hasło).</param>
    /// <returns>Obiekt <see cref="AuthResultDto"/> zawierający token i dane użytkownika.</returns>
    /// <exception cref="UnauthorizedAccessException">W przypadku nieprawidłowego loginu lub hasła.</exception>
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

    /// <summary>
    /// Rejestruje nowego użytkownika z unikalnym adresem e-mail i zaszyfrowanym hasłem.
    /// </summary>
    /// <param name="dto">Dane rejestracyjne użytkownika.</param>
    /// <exception cref="InvalidOperationException">Jeśli użytkownik o danym adresie e-mail już istnieje.</exception>
    public async Task RegisterAsync(UserRegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = passwordHash;

        await _userRepository.AddAsync(user);
    }
}
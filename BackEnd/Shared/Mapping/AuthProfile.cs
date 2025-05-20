using AutoMapper;
using BackEnd.Domain.Entities;
using BackEnd.Application.DTOs;

namespace BackEnd.Shared.Mappings;

/// <summary>
/// Profil mapujący dane związane z autoryzacją i użytkownikami.
/// Wykorzystywany przez AutoMapper do konwersji między encjami domenowymi a DTO.
/// </summary>
public class AuthProfile : Profile
{
    /// <summary>
    /// Konfiguruje mapowania między typami User i DTO wykorzystywanymi w procesach logowania i rejestracji.
    /// </summary>
    public AuthProfile()
    {
        // Mapowanie z encji User do DTO prezentującego dane użytkownika
        CreateMap<User, UserDto>();

        // Mapowanie z DTO rejestracyjnego na encję User (np. przy tworzeniu nowego użytkownika)
        CreateMap<UserRegisterDto, User>();

        // Mapowanie z DTO logowania na encję User (np. do weryfikacji danych)
        CreateMap<UserLoginDto, User>();
    }
}
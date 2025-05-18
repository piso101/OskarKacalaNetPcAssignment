using AutoMapper;
using BackEnd.Domain.Entities;
using BackEnd.Application.DTOs;

namespace BackEnd.Shared.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<UserLoginDto, User>();
    }
}
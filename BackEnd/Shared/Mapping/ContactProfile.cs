using AutoMapper;
using BackEnd.Domain.Entities;
using BackEnd.Application.DTOs;

namespace BackEnd.Shared.Mappings;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory != null ? src.Subcategory.Name : src.CustomSubcategory));

        CreateMap<CreateContactDto, Contact>();
        CreateMap<UpdateContactDto, Contact>();
    }
}

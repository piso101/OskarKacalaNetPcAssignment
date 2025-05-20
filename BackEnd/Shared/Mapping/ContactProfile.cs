using AutoMapper;
using BackEnd.Domain.Entities;
using BackEnd.Application.DTOs;

namespace BackEnd.Shared.Mappings;

/// <summary>
/// Profil mapujący dane kontaktowe między encjami domenowymi a DTO.
/// Używany przez AutoMapper do konwersji danych podczas operacji na kontaktach.
/// </summary>
public class ContactProfile : Profile
{
    /// <summary>
    /// Konfiguruje mapowania AutoMappera dla encji Contact oraz odpowiadających jej DTO.
    /// Uwzględnia logikę transformacji danych z relacyjnych encji (np. Category, Subcategory).
    /// </summary>
    public ContactProfile()
    {
        // Mapowanie z encji Contact do DTO z rozwiniętymi nazwami kategorii i subkategorii
        CreateMap<Contact, ContactDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src =>
                src.Subcategory != null ? src.Subcategory.Name : src.CustomSubcategory));

        // Mapowanie z DTO tworzenia kontaktu do encji domenowej Contact
        CreateMap<CreateContactDto, Contact>();

        // Mapowanie z DTO aktualizacji kontaktu do encji Contact
        CreateMap<UpdateContactDto, Contact>();
    }
}
using AutoMapper;
using LibraryService.Application.Dto_s.Author;
using LibraryService.Application.Dto_s.Author.Create;
using LibraryService.Application.Dto_s.Author.Update;
using LibraryService.Core.Entities;

namespace LibraryService.Application.Mappings;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorCreateRequestDto, AuthorEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid())).
            ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName)).
            ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName)).
            ForMember(dest => dest.CountryCode,
                opt => opt.MapFrom(src => src.CountryCode)).
            ForMember(dest => dest.BirthDay,
                opt => opt.MapFrom(src => src.BirthDay));

        CreateMap<AuthorUpdateRequestDto, AuthorEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)).
            ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName)).
            ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName)).
            ForMember(dest => dest.CountryCode,
                opt => opt.MapFrom(src => src.CountryCode)).
            ForMember(dest => dest.BirthDay,
                opt => opt.MapFrom(src => src.BirthDay)).
            ForMember(dest => dest.Books,
                opt => opt.MapFrom(src => src.Books));

        CreateMap<AuthorEntity, AuthorCreateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));

        CreateMap<AuthorEntity, AuthorUpdateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
    }
}
using AutoMapper;
using LibraryService.Application.Dto_s.Auth;
using LibraryService.Application.Dto_s.Auth.Register;
using LibraryService.Core.Entities;
using LibraryService.Core.Responses;

namespace LibraryService.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequestDto, UserEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid())).
            ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)).
            ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email)).
            ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => src.Password));

        CreateMap<AuthResponse, RegisterResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)).
            ForMember(dest => dest.AccessToken,
                opt => opt.MapFrom(src => src.AccessToken)).
            ForMember(dest => dest.RefreshToken,
                opt => opt.MapFrom(src => src.RefreshToken));

        CreateMap<AuthResponse, LoginResponseDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AccessToken,
                opt => opt.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.RefreshToken,
                opt => opt.MapFrom(src => src.RefreshToken));

        CreateMap<AuthResponse, RefreshResponseDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AccessToken,
                opt => opt.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.RefreshToken,
                opt => opt.MapFrom(src => src.RefreshToken));
    }
}
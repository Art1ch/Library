using AutoMapper;
using LibraryService.Application.Dto_s.User;
using LibraryService.Core.Entities;
using LibraryService.Core.Roles;

namespace LibraryService.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateRequestDto, UserEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid())).
            ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email)).
            ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)).
            ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => src.Password)).
            ForMember(dest => dest.Role,
                opt => opt.MapFrom(_ => Role.User));

        CreateMap<UserUpdateRequestDto, UserEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)).
            ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email)). 
            ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)).
            ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.Role));

        CreateMap<UserEntity, UserCreateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));

        CreateMap<UserEntity, UserUpdateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
    }
}
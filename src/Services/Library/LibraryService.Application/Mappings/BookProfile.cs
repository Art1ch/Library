using AutoMapper;
using LibraryService.Application.Dto_s.Book.Create;
using LibraryService.Application.Dto_s.Book.Update;
using LibraryService.Core.Entities;

namespace LibraryService.Application.Mappings;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookCreateRequestDto, BookEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid())).
            ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.AuthorId)).
            ForMember(dest => dest.ISBN,
                opt => opt.MapFrom(src => src.ISBN)).
            ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)).
            ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre)).
            ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description));
        //ForMember(dest => dest.Image,
        //    opt => opt.MapFrom(src => src.Image));

        CreateMap<BookUpdateRequestDto, BookEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.BookId)).
            ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.AuthorId)).
            ForMember(dest => dest.ISBN,
                opt => opt.MapFrom(src => src.ISBN)).
            ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)).
            ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre)).
            ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description));
            //ForMember(dest => dest.Image,
            //    opt => opt.MapFrom(src => src.Image));

        CreateMap<BookEntity, BookCreateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));

        CreateMap<BookEntity, BookUpdateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
    }
}
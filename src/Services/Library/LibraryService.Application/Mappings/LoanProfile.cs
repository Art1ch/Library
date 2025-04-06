using AutoMapper;
using LibraryService.Application.Dto_s.Loan.Create;
using LibraryService.Application.Dto_s.Loan.Update;
using LibraryService.Core.Entities;

namespace LibraryService.Application.Mappings;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<LoanCreateRequestDto, LoanEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid())).
            ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId)).
            ForMember(dest => dest.TakenDate,
                opt => opt.MapFrom(_ => GetTodayDate())).
            ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => GetTodayDate().
                AddDays(src.AmountOfDays)));

        CreateMap<LoanUpdateRequestDto, LoanEntity>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)).
            ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId)).
            ForMember(dest => dest.BookId,
                opt => opt.MapFrom(src => src.BookId)).
            ForMember(dest => dest.TakenDate,
                opt => opt.MapFrom(src => src.TakenDate)).
            ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => src.DueDate));

        CreateMap<LoanEntity, LoanCreateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));

        CreateMap<LoanEntity, LoanUpdateResponseDto>().
            ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
    }

    private DateOnly GetTodayDate()
    {
        return new DateOnly(
            DateTime.UtcNow.Year,
            DateTime.UtcNow.Month,
            DateTime.UtcNow.Day);
    }
}

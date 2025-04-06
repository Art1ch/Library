namespace LibraryService.Application.Dto_s.Loan.Create;

public record LoanCreateRequestDto(
    Guid UserId,
    Guid BookId,
    int AmountOfDays);
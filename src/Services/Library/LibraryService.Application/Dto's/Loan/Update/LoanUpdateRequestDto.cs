namespace LibraryService.Application.Dto_s.Loan.Update;

public record LoanUpdateRequestDto(
    Guid Id,
    Guid UserId,
    Guid BookId,
    DateOnly TakenDate,
    DateOnly DueDate);
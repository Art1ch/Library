using LibraryService.Application.Dto_s.Loan.Update;
using MediatR;

namespace LibraryService.Application.Commands.Loan.Update;

public record LoanUpdateCommand(
    LoanUpdateRequestDto Dto) : IRequest<LoanUpdateResponseDto>;

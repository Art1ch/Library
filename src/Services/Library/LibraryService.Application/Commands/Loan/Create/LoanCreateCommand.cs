using LibraryService.Application.Dto_s.Loan.Create;
using MediatR;

namespace LibraryService.Application.Commands.Loan.Create;

public record LoanCreateCommand(
    LoanCreateRequestDto Dto) : IRequest<LoanCreateResponseDto>;
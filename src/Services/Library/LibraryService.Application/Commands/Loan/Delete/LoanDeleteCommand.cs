using LibraryService.Application.Dto_s.Loan.Delete;
using MediatR;

namespace LibraryService.Application.Commands.Loan.Delete;

public record LoanDeleteCommand(
    LoanDeleteRequestDto Dto) : IRequest<LoanDeleteResponseDto>;

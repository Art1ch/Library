using LibraryService.Application.Dto_s.Loan.Delete;
using LibraryService.Core.Contracts.Repositories;
using MediatR;

namespace LibraryService.Application.Commands.Loan.Delete;

public class LoanDeleteCommandHandler :
    IRequestHandler<LoanDeleteCommand, LoanDeleteResponseDto>
{
    private readonly ILoanRepository _loanRepository;
    public LoanDeleteCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanDeleteResponseDto> Handle(LoanDeleteCommand request, CancellationToken cancellationToken)
    {
        await _loanRepository.DeleteLoanAsync(request.Dto.Id, cancellationToken);
        return new LoanDeleteResponseDto();
    }
}

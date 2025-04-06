using LibraryService.Application.Queries.Loan.GetById;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Loan.GetAllByUserId;

public class LoansGetByUserIdQueryHandler :
    IRequestHandler<LoansGetByUserIdQuery, List<LoanEntity>>
{
    private readonly ILoanRepository _loanRepository;

    public LoansGetByUserIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<List<LoanEntity>> Handle(LoansGetByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _loanRepository.GetLoansAsync(request.UserId);
    }
}

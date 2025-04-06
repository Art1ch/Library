using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Application.Queries.Loan.GetById;

public class LoanGetByIdQueryHandler :
    IRequestHandler<LoanGetByIdQuery, LoanEntity>
{
    private readonly ILoanRepository _loanRepository;
    public LoanGetByIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanEntity> Handle(LoanGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _loanRepository.GetLoanAsync(request.Id);
    }
}

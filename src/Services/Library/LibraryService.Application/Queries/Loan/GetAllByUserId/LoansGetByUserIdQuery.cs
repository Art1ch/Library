using LibraryService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Application.Queries.Loan.GetAllByUserId;

public record LoansGetByUserIdQuery(
    Guid UserId) : IRequest<List<LoanEntity>>;

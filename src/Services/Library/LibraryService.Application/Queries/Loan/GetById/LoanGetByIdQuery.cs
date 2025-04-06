using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Loan.GetById;

public record LoanGetByIdQuery(
    Guid Id) : IRequest<LoanEntity>;

using LibraryService.Core.Entities;

namespace LibraryService.Core.Contracts.Repositories;

public interface ILoanRepository
{
    public Task<List<LoanEntity>> GetLoansAsync(Guid userId, CancellationToken ct = default);
    public Task<LoanEntity> GetLoanAsync(Guid loanId, CancellationToken ct = default);
    public Task<Guid> CreateLoanAsync(LoanEntity bookLoan, CancellationToken ct = default);
    public Task<Guid> UpdateLoanAsync(LoanEntity bookLoan, CancellationToken ct = default);
    public Task DeleteLoanAsync(Guid loanId, CancellationToken ct = default);
    public Task DeleteLoanAsync(Guid userId, Guid bookId, CancellationToken ct = default);
}
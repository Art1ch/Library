using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using LibraryService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryContext _context;

    public LoanRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateLoanAsync(LoanEntity bookLoan, CancellationToken ct = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == bookLoan.UserId, ct)
            ?? throw new Exception("Invalid user id");
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookLoan.BookId, ct)
            ?? throw new Exception("Invalid book id");

        bookLoan.User = user;
        bookLoan.Book = book;

        await _context.Loans.AddAsync(bookLoan, ct);
        await _context.SaveChangesAsync(ct);

        return bookLoan.Id;
    }

    public async Task DeleteLoanAsync(Guid loanId, CancellationToken ct = default)
    {
        await _context.Loans.
            Where(l => l.Id == loanId).ExecuteDeleteAsync(ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteLoanAsync(Guid userId, Guid bookId, CancellationToken ct = default)
    {
        await _context.Loans.
            Where(l => l.UserId == userId).
            Where(l => l.BookId == bookId).
            ExecuteDeleteAsync();
        await _context.SaveChangesAsync(ct);
    }

    public async Task<LoanEntity> GetLoanAsync(Guid loanId, CancellationToken ct = default)
    {
        var loan = await _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == loanId)
            ?? throw new NullReferenceException("Invalid loan id");
        return loan;
    }

    public async Task<List<LoanEntity>> GetLoansAsync(Guid userId, CancellationToken ct = default)
    {
        var loans = await _context.Loans.Where(l => l.UserId == userId)
            .Include(l => l.User)
            .Include(l => l.Book)
            .ToListAsync()
            ?? throw new NullReferenceException("Invalid user id");
        return loans;
    }

    public async Task<Guid> UpdateLoanAsync(LoanEntity bookLoan, CancellationToken ct = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == bookLoan.UserId)
            ?? throw new NullReferenceException("Invalid user id");
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookLoan.BookId)
            ?? throw new NullReferenceException("Invalid book id");

        bookLoan.User = user;
        bookLoan.Book = book;

        await _context.Loans.
            Where(l => l.Id == bookLoan.Id).
            ExecuteUpdateAsync
            (
            setter => setter.
            SetProperty(l => l.UserId, bookLoan.UserId).
            SetProperty(l => l.BookId, bookLoan.BookId).
            SetProperty(l => l.User, bookLoan.User).
            SetProperty(l => l.Book, bookLoan.Book).
            SetProperty(l => l.TakenDate, bookLoan.TakenDate).
            SetProperty(l => l.DueDate, bookLoan.DueDate),
            ct);
        await _context.SaveChangesAsync(ct);

        return bookLoan.Id;
    }
}
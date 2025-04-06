using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using LibraryService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryContext _context;
    public AuthorRepository(LibraryContext context)
    {
        _context = context;                 
    }
    public async Task<Guid> CreateAuthorAsync(AuthorEntity author, CancellationToken ct = default)
    {
        await _context.Authors.AddAsync(author, ct);
        await _context.SaveChangesAsync(ct);
        return author.Id;
    }

    public async Task DeleteAuthorAsync(Guid id, CancellationToken ct = default)
    {
        await _context.Authors.
            Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<AuthorEntity>> GetAuthorsAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var users = await _context.Authors
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(b => b.Books)
            .ToListAsync(ct);
        return users;
    }

    public async Task<AuthorEntity> GetAuthorByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Authors
            .Include(b => b.Books)
            .FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NullReferenceException("Invalid id");
    }

    public async Task<Guid> UpdateAuthorAsync(AuthorEntity author, CancellationToken ct = default)
    {
        await _context.Authors
            .Where(a => a.Id == author.Id)
            .ExecuteUpdateAsync(
            setter => setter.
            SetProperty(a => a.FirstName, author.FirstName).
            SetProperty(a => a.LastName, author.LastName).
            SetProperty(a => a.BirthDay, author.BirthDay).
            SetProperty(a => a.CountryCode, author.CountryCode).
            SetProperty(a => a.Books, author.Books),
            ct);
        await _context.SaveChangesAsync(ct);
        return author.Id;
    }
}
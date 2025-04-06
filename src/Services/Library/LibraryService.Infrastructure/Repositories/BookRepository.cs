using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using LibraryService.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateBookAsync(BookEntity book, CancellationToken ct = default)
    {

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId)
            ?? throw new NullReferenceException("Invalid author id");

        book.Author = author;
        _context.Books.Add(book);

        await _context.SaveChangesAsync(ct);

        return book.Id;
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken ct = default)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id) 
            ?? throw new NullReferenceException("Invalid book id");

        await _context.Books.
            Where(b => b.Id == id).
            ExecuteDeleteAsync(ct);

        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<BookEntity>> GetBooksByAuthorIdAsync(Guid authorId, CancellationToken ct = default)
    {
        var author = await _context.Authors
            .Include(b => b.Books)
            .FirstOrDefaultAsync(a => a.Id == authorId)
            ?? throw new NullReferenceException("Invalid author id");
        return author.Books;
    }

    public async Task<BookEntity> GetBookByIdAsync(Guid id, CancellationToken ct = default)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id, ct);
        return book;
    }

    public async Task<BookEntity> GetBookByISBNAsync(string isbn, CancellationToken ct = default)
    {
        var book = await _context.Books.
            FirstOrDefaultAsync(b => b.ISBN == isbn, ct);
        return book;
    }

    public Task<List<BookEntity>> GetBooksAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var books = _context.Books.AsQueryable().
            Skip((page - 1) * pageSize).
            Take(pageSize).
            ToListAsync(ct);
        return books;
    }

    public async Task<Guid> UpdateBookAsync(BookEntity book, CancellationToken ct = default)
    {
        var author = book.Author;

        var oldBook = _context.Books.FirstOrDefault(b => b.Id == book.Id)
                ?? throw new NullReferenceException("Invalid book id");
        var newBook = book;

        await _context.Books.
            Where(b => b.Id == book.Id).
            ExecuteUpdateAsync(
            setter => setter.
            SetProperty(b => b.Name, book.Name).
            SetProperty(b => b.ISBN, book.ISBN).
            SetProperty(b => b.AuthorId, book.AuthorId).
            SetProperty(b => b.Author, book.Author).
            SetProperty(b => b.Description, book.Description).
            SetProperty(b => b.Genre, book.Genre),
            //SetProperty(b => b.Image, book.Image),
            ct);

        await _context.SaveChangesAsync(ct);

        return book.Id;
    }

    //public async Task UploadImageAsync(Guid id, byte[] image, CancellationToken ct = default)
    //{
    //    await using var transaction = await _context.Database.BeginTransactionAsync(ct);
    //    try
    //    {
    //        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id)
    //            ?? throw new Exception("Invalid book id");

    //        await _context.Books.
    //           Where(b => b.Id == id).
    //           ExecuteUpdateAsync(
    //           setter => setter.
    //           SetProperty(b => b.Image, image));

    //        await _context.SaveChangesAsync(ct);
    //        await transaction.CommitAsync(ct);
    //    }
    //    catch (Exception ex)
    //    {
    //        await transaction.RollbackAsync();
    //        throw;
    //    }
    //}
}

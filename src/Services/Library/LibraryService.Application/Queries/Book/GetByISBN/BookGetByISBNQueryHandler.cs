using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetByISBN;

public class BookGetByISBNQueryHandler :
    IRequestHandler<BookGetByISBNQuery, BookEntity>
{
    private readonly IBookRepository _bookRepository;

    public BookGetByISBNQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookEntity> Handle(BookGetByISBNQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetBookByISBNAsync(request.ISBN, cancellationToken);
    }
}
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetAll;

public class BooksGetQueryHandler :
    IRequestHandler<BooksGetQuery, List<BookEntity>>
{
    private readonly IBookRepository _bookRepository;
    public BooksGetQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookEntity>> Handle(BooksGetQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetBooksAsync(request.Page, request.PageSize, cancellationToken);
    }
}
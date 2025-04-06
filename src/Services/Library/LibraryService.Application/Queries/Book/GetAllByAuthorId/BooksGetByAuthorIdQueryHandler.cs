using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetAllByAuthorId;

public class BooksGetByAuthorIdQueryHandler :
    IRequestHandler<BooksGetByAuthorIdQuery, List<BookEntity>>
{
    private readonly IBookRepository _bookRepository;
    public BooksGetByAuthorIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookEntity>> Handle(BooksGetByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetBooksByAuthorIdAsync(request.AuthorId, cancellationToken);
    }
}

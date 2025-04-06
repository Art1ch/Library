using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetById;

public class BookGetByIdQueryHandler :
    IRequestHandler<BookGetByIdQuery, BookEntity>
{
    private readonly IBookRepository _bookRepository;

    public BookGetByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookEntity> Handle(BookGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetBookByIdAsync(request.Id, cancellationToken);
    }
}
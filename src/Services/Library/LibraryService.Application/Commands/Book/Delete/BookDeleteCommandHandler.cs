using AutoMapper;
using LibraryService.Application.Dto_s.Book.Delete;
using LibraryService.Core.Contracts.Repositories;
using MediatR;

namespace LibraryService.Application.Commands.Book.Delete;

public class BookDeleteCommandHandler :
    IRequestHandler<BookDeleteCommand, BookDeleteResponseDto>
{
    private readonly IBookRepository _bookRepository;
    public BookDeleteCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
    }
    public async Task<BookDeleteResponseDto> Handle(BookDeleteCommand request, CancellationToken cancellationToken)
    {
        await _bookRepository.DeleteBookAsync(request.Dto.Id, cancellationToken);
        return new BookDeleteResponseDto();
    }
}
using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Book.Update;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Book.Update;

public class BookUpdateCommandHandler :
    IRequestHandler<BookUpdateCommand, BookUpdateResponseDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookEntity> _validator;

    public BookUpdateCommandHandler(
        IBookRepository bookRepository,
        IMapper mapper,
        IValidator<BookEntity> validator)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookUpdateResponseDto> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<BookEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _bookRepository.UpdateBookAsync(book, cancellationToken);
        return _mapper.Map<BookUpdateResponseDto>(book);
    }
}

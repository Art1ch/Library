using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Book.Create;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Book.Create;

public class BookCreateCommandHandler :
    IRequestHandler<BookCreateCommand, BookCreateResponseDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookEntity> _validator;
    public BookCreateCommandHandler(
        IBookRepository bookRepository, 
        IMapper mapper, 
        IValidator<BookEntity> validator)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookCreateResponseDto> Handle(BookCreateCommand request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<BookEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _bookRepository.CreateBookAsync(book, cancellationToken);
        return _mapper.Map<BookCreateResponseDto>(book);
    }
}
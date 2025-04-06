using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Author.Create;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Author.Create;

public class AuthorCreateCommandHandler :
    IRequestHandler<AuthorCreateCommand, AuthorCreateResponseDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AuthorEntity> _validator;
    public AuthorCreateCommandHandler(
        IAuthorRepository authorRepository,
        IMapper mapper,
        IValidator<AuthorEntity> validator)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<AuthorCreateResponseDto> Handle(AuthorCreateCommand request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<AuthorEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(author);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _authorRepository.CreateAuthorAsync(author, cancellationToken);
        return _mapper.Map<AuthorCreateResponseDto>(author);
    }
}
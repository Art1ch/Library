using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Author.Update;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Author.Update;

public class AuthorUpdateCommandHandler :
    IRequestHandler<AuthorUpdateCommand, AuthorUpdateResponseDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AuthorEntity> _validator;
    public AuthorUpdateCommandHandler(
        IAuthorRepository authorRepository,
        IMapper mapper,
        IValidator<AuthorEntity> validator)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<AuthorUpdateResponseDto> Handle(AuthorUpdateCommand request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<AuthorEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(author);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _authorRepository.UpdateAuthorAsync(author, cancellationToken);
        return _mapper.Map<AuthorUpdateResponseDto>(author);
    }
}

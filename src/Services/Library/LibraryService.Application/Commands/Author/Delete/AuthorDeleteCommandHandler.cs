using AutoMapper;
using LibraryService.Application.Dto_s.Author.Delete;
using LibraryService.Core.Contracts.Repositories;
using MediatR;

namespace LibraryService.Application.Commands.Author.Delete;

public class AuthorDeleteCommandHandler :
    IRequestHandler<AuthorDeleteCommand, AuthorDeleteResponseDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorDeleteCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<AuthorDeleteResponseDto> Handle(AuthorDeleteCommand request, CancellationToken cancellationToken)
    {
        await _authorRepository.DeleteAuthorAsync(request.Dto.Id, cancellationToken);
        return new AuthorDeleteResponseDto();
    }
}
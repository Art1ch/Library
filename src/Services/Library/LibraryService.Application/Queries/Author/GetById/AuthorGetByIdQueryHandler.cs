using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Author.GetById;

public class AuthorGetByIdQueryHandler :
    IRequestHandler<AuthorGetByIdQuery, AuthorEntity>
{
    private readonly IAuthorRepository _authoRepository;

    public AuthorGetByIdQueryHandler(IAuthorRepository authoRepository)
    {
        _authoRepository = authoRepository;
    }

    public async Task<AuthorEntity> Handle(AuthorGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _authoRepository.GetAuthorByIdAsync(request.Id);
    }
}

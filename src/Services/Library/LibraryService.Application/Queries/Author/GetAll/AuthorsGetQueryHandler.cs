using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Author.GetAll;

public class AuthorsGetQueryHandler :
    IRequestHandler<AuthorsGetQuery, List<AuthorEntity>>
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorsGetQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<AuthorEntity>> Handle(AuthorsGetQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAuthorsAsync(request.Page, request.PageSize, cancellationToken);
    }
}

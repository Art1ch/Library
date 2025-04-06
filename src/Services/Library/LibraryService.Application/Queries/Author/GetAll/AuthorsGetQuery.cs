using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Author.GetAll;

public record AuthorsGetQuery(
    int Page,
    int PageSize) : IRequest<List<AuthorEntity>>;
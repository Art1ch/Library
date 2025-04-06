using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Author.GetById;

public record AuthorGetByIdQuery(
    Guid Id) : IRequest<AuthorEntity>;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetAllByAuthorId;

public record BooksGetByAuthorIdQuery(
    Guid AuthorId) : IRequest<List<BookEntity>>;
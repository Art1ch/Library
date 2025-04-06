using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetAll;

public record BooksGetQuery(
    int Page,
    int PageSize) : IRequest<List<BookEntity>>;
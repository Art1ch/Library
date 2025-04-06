using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetById;

public record BookGetByIdQuery(
    Guid Id) : IRequest<BookEntity>;

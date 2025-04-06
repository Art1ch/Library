using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.Book.GetByISBN;

public record BookGetByISBNQuery(
    string ISBN) : IRequest<BookEntity>;
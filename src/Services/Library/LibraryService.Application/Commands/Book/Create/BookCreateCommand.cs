using LibraryService.Application.Dto_s.Book.Create;
using MediatR;

namespace LibraryService.Application.Commands.Book.Create;

public record BookCreateCommand(
    BookCreateRequestDto Dto) : IRequest<BookCreateResponseDto>;
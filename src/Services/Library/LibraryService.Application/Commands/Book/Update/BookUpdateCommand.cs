using LibraryService.Application.Dto_s.Book.Update;
using MediatR;

namespace LibraryService.Application.Commands.Book.Update;

public record BookUpdateCommand(
    BookUpdateRequestDto Dto) : IRequest<BookUpdateResponseDto>;
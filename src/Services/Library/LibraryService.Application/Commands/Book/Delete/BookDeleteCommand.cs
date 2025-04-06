using LibraryService.Application.Dto_s.Book.Create;
using LibraryService.Application.Dto_s.Book.Delete;
using MediatR;

namespace LibraryService.Application.Commands.Book.Delete;

public record BookDeleteCommand(
    BookDeleteRequestDto Dto) : IRequest<BookDeleteResponseDto>;
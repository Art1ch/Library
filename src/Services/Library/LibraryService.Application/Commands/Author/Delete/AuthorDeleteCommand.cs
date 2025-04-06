using LibraryService.Application.Dto_s.Author;
using LibraryService.Application.Dto_s.Author.Delete;
using MediatR;

namespace LibraryService.Application.Commands.Author.Delete;

public record AuthorDeleteCommand(
    AuthorDeleteRequestDto Dto) : IRequest<AuthorDeleteResponseDto>;
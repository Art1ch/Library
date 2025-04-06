using LibraryService.Application.Dto_s.Author;
using LibraryService.Application.Dto_s.Author.Create;
using MediatR;

namespace LibraryService.Application.Commands.Author.Create;

public record AuthorCreateCommand(
    AuthorCreateRequestDto Dto) : IRequest<AuthorCreateResponseDto>;
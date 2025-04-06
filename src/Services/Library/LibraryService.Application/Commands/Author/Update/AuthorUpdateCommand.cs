using LibraryService.Application.Dto_s.Author;
using LibraryService.Application.Dto_s.Author.Update;
using MediatR;

namespace LibraryService.Application.Commands.Author.Update;

public record AuthorUpdateCommand(
    AuthorUpdateRequestDto Dto) : IRequest<AuthorUpdateResponseDto>;
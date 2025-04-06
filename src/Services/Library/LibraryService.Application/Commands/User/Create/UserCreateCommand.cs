using LibraryService.Application.Dto_s.User;
using MediatR;

namespace LibraryService.Application.Commands.User.Create;

public record UserCreateCommand(
    UserCreateRequestDto Dto) : IRequest<Guid>;
using LibraryService.Application.Dto_s.User;
using MediatR;

namespace LibraryService.Application.Commands.User.Update;

public record UserUpdateCommand(UserUpdateRequestDto Dto) : IRequest<Guid>;
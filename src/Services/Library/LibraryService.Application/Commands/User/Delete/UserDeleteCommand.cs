using LibraryService.Application.Dto_s.User;
using MediatR;

namespace LibraryService.Application.Commands.User.Delete;

public record UserDeleteCommand(
    UserDeleteRequestDto Dto) : IRequest<UserDeleteResponseDto>;
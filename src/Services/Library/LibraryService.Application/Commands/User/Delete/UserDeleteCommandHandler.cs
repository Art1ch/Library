using LibraryService.Application.Dto_s.User;
using LibraryService.Core.Contracts.Repositories;
using MediatR;

namespace LibraryService.Application.Commands.User.Delete;

public record UserDeleteCommandHandler :
    IRequestHandler<UserDeleteCommand, UserDeleteResponseDto>
{
    private readonly IUserRepository _userRepository;

    public UserDeleteCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDeleteResponseDto> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUserAsync(request.Dto.Id, cancellationToken);
        return new UserDeleteResponseDto();
    }
}

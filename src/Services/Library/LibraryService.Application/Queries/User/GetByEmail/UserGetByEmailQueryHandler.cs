using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.User.GetByEmail;

public class UserGetByEmailQueryHandler : IRequestHandler<UserGetByEmailQuery, UserEntity>
{
    private readonly IUserRepository _userRepository;

    public UserGetByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserEntity> Handle(UserGetByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
    }
}

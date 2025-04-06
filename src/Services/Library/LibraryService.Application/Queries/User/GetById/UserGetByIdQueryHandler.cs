using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.User.GetById;

public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserEntity>
{
    private readonly IUserRepository _userRepository;

    public UserGetByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserEntity> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(request.Id);
    }
}

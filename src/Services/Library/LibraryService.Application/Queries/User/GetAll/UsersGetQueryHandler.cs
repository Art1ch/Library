using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.User.GetAll;

public class UsersGetQueryHandler : IRequestHandler<UsersGetQuery, List<UserEntity>>
{
    private readonly IUserRepository _userRepository;
    public UsersGetQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserEntity>> Handle(UsersGetQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUsersAsync(request.Page, request.PageSize, cancellationToken); 
    }
}

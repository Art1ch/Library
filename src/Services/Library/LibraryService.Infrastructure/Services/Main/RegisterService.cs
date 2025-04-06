using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Contracts.Services.Additional;
using LibraryService.Core.Contracts.Services.Main;
using LibraryService.Core.Entities;
using LibraryService.Core.Responses;

namespace LibraryService.Infrastructure.Services.Main;

public class RegisterService : IRegisterService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    public RegisterService(IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(UserEntity user)
    {
        if (await _userRepository.GetUserByEmailAsync(user.Email) is not null)
        {
            throw new Exception("User with this email already exists");
        }
        user.Password = _passwordHasher.GeneratePassword(user.Password);
        await _userRepository.CreateUserAsync(user);
    }
}
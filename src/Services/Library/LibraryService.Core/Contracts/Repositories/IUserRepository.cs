using LibraryService.Core.Entities;

namespace LibraryService.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task<List<UserEntity>> GetUsersAsync(int page, int pageSize, CancellationToken ct = default);
    Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken ct = default);
    Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken ct = default);
    Task<Guid> CreateUserAsync(UserEntity user, CancellationToken ct = default);
    Task<Guid> UpdateUserAsync(UserEntity user, CancellationToken ct = default);
    Task DeleteUserAsync(Guid id, CancellationToken ct = default);
}
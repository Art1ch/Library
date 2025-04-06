using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using LibraryService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LibraryContext _context;
    public UserRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateUserAsync(UserEntity user, CancellationToken ct = default)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(ct);
        return user.Id;
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken ct = default)
    {
        await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync(ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<UserEntity>> GetUsersAsync(int page, int pageSize, CancellationToken ct = default)
    {
       var users = await _context.Users
                .AsQueryable()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        return users;
    }

    public async Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, ct)
            ?? throw new NullReferenceException("Invalid user id");
        return user;
    }

    public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken ct = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, ct)
            ?? throw new NullReferenceException("Invalid email");
        return user;
    }

    public async Task<Guid> UpdateUserAsync(UserEntity user, CancellationToken ct = default)
    {
        await _context.Users.Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync
            (
            setter => setter
            .SetProperty(u => u.Name, user.Name)             
            .SetProperty(u => u.Role, user.Role),
            ct);
        await _context.SaveChangesAsync(ct);
        return user.Id; 
    }
}
using LibraryService.Core.Roles;

namespace LibraryService.Core.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public Role Role { get; set; } = Role.User;
    public List<LoanEntity> Loans { get; set; } = new();
}
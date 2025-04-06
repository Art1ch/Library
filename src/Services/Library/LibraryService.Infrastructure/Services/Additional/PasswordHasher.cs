using LibraryService.Core.Contracts.Services.Additional;

namespace LibraryService.Infrastructure.Services.Additional;

public class PasswordHasher : IPasswordHasher
{
    public string GeneratePassword(string password)
    {
        return BCrypt.Net.
            BCrypt.EnhancedHashPassword(password, 12, BCrypt.Net.HashType.SHA256);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword, BCrypt.Net.HashType.SHA256);
    }
}
namespace LibraryService.Core.Contracts.Services.Additional;

public interface IPasswordHasher
{
    public string GeneratePassword(string password);
    public bool VerifyPassword(string password, string hashedPassword);
}
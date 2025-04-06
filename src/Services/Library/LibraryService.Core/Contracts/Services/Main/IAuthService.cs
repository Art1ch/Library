using LibraryService.Core.Responses;

namespace LibraryService.Core.Contracts.Services.Main;

public interface IAuthService
{
    public Task<AuthResponse> LoginAsync(string email, string password, CancellationToken ct = default);
    public Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken ct = default);
}

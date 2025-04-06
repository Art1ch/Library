using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Contracts.Services.Additional;
using LibraryService.Core.Contracts.Services.Main;
using LibraryService.Core.Entities;
using LibraryService.Core.Responses;
using LibraryService.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LibraryService.Infrastructure.Services.Main;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenProvider jwtTokenProvider,
        JwtSettings jwtSettings)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenProvider = jwtTokenProvider;
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password, CancellationToken ct)
    {
        if (email == "admin" && password == "admin") // For admin, shit code
        {
            var adminUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                Password = "admin",
                Name = "admin",
                Email = "admin",
                Role = Core.Roles.Role.Admin, 
            };

            var adminAccessToken = _jwtTokenProvider.GenerateAccessToken(adminUser);
            var adminRefreshToken = _jwtTokenProvider.GenerateRefreshToken();


            return new AuthResponse(adminUser.Id, adminAccessToken, adminRefreshToken);
        }

        var user = await _userRepository.GetUserByEmailAsync(email)
            ?? throw new Exception("User not found");

        if (!_passwordHasher.VerifyPassword(password, user.Password))
            throw new Exception("Invalid password");

        var accessToken = _jwtTokenProvider.GenerateAccessToken(user);
        var refreshToken = _jwtTokenProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
        await _userRepository.UpdateUserAsync(user);

        return new AuthResponse(user.Id, accessToken, refreshToken);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken ct)
    {
        var principal = _jwtTokenProvider.ValidateToken(accessToken);
        if (principal == null)
        {
            throw new SecurityTokenException("Invalid token");
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new Exception("Invalid token claims");
        }

        var user = await _userRepository.GetUserByIdAsync(userId, ct);

        if (user == null || user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            throw new Exception("Invalid refresh token");
        }

        var newAccessToken = _jwtTokenProvider.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenProvider.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userRepository.UpdateUserAsync(user, ct);

        return new AuthResponse(user.Id, newAccessToken, newRefreshToken);
    }
}
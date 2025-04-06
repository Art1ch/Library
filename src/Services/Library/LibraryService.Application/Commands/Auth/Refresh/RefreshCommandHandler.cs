using AutoMapper;
using LibraryService.Application.Dto_s.Auth;
using LibraryService.Core.Contracts.Services.Main;
using MediatR;

namespace LibraryService.Application.Commands.Auth.Refresh;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RefreshCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<RefreshResponseDto> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.RefreshTokenAsync(request.Dto.AccessToken, request.Dto.RefreshToken);
        return _mapper.Map<RefreshResponseDto>(response);
    }
}

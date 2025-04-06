using LibraryService.Application.Dto_s.Auth;
using LibraryService.Application.Dto_s.Auth.Login;
using MediatR;

namespace LibraryService.Application.Commands.Auth.Login;

public record LoginCommand(
    LoginRequestDto Dto) : IRequest<LoginResponseDto>;
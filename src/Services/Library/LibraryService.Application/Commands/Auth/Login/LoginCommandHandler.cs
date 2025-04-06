using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Auth;
using LibraryService.Application.Dto_s.Auth.Login;
using LibraryService.Core.Contracts.Services.Main;
using MediatR;

namespace LibraryService.Application.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IValidator<LoginRequestDto> _validator;

    public LoginCommandHandler(IAuthService authService, IMapper mapper, IValidator<LoginRequestDto> validator)
    {
        _authService = authService;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var response = await _authService.LoginAsync(request.Dto.Email, request.Dto.Password, cancellationToken);
        return _mapper.Map<LoginResponseDto>(response);
    }
}

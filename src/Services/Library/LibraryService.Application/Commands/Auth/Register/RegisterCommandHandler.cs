using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Auth.Register;
using LibraryService.Core.Contracts.Services.Main;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IRegisterService _registerService;
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterRequestDto> _validator;
    public RegisterCommandHandler(
        IMapper mapper,
        IAuthService authService,
        IRegisterService registerService,
        IValidator<RegisterRequestDto> validator)
    {
        _mapper = mapper;
        _authService = authService;
        _registerService = registerService;
        _validator = validator;
    }

    public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = _mapper.Map<UserEntity>(request.Dto);
        var notHashedPassword = user.Password;
        await _registerService.RegisterAsync(user);
        var loginResponse = await _authService.LoginAsync(user.Email, notHashedPassword);
        return _mapper.Map<RegisterResponseDto>(loginResponse);
    }
}
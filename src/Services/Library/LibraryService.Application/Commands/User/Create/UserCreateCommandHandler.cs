using AutoMapper;
using FluentValidation;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Contracts.Services.Additional;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.User.Create;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IValidator<UserEntity> _validator;
    public UserCreateCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper,
        IValidator<UserEntity> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var hashedPassword = _passwordHasher.GeneratePassword(user.Password);
        user.Password = hashedPassword;
        await _userRepository.CreateUserAsync(user, cancellationToken);
        return user.Id;
    }
}
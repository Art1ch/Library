using AutoMapper;
using FluentValidation;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.User.Update;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserEntity> _validator;
    public UserUpdateCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<UserEntity> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Guid> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _userRepository.UpdateUserAsync(user, cancellationToken);
        return user.Id;
    }
}
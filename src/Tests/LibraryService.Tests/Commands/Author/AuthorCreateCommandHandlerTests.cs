using LibraryService.Application.Queries.Author.GetById;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using Moq;
using FluentAssertions;
using LibraryService.Application.Dto_s.Author;
using AutoMapper;
using FluentValidation;
using LibraryService.Application.Commands.Author.Create;
using FluentValidation.Results;
using LibraryService.Application.Dto_s.Author.Create;

namespace LibraryService.Tests.Commands.Books;

public class AuthorCreateCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<AuthorEntity>> _validatorMock;
    private readonly AuthorCreateCommandHandler _handler;

    public AuthorCreateCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<AuthorEntity>>();
        _handler = new AuthorCreateCommandHandler(
            _repositoryMock.Object,
            _mapperMock.Object,
            _validatorMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesAuthorAndReturnsResponse()
    {
        var requestDto = new AuthorCreateRequestDto(
            FirstName: "John",
            LastName: "Doe",
            CountryCode: "US",
            BirthDay: new DateOnly(1980, 1, 1));

        var command = new AuthorCreateCommand(requestDto);
        var generatedId = Guid.NewGuid();

        var authorEntity = new AuthorEntity
        {
            Id = generatedId,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            CountryCode = requestDto.CountryCode,
            BirthDay = requestDto.BirthDay
        };

        _mapperMock
            .Setup(m => m.Map<AuthorEntity>(requestDto))
            .Returns(authorEntity);

        var expectedResponse = new AuthorCreateResponseDto(generatedId);
        _mapperMock
            .Setup(m => m.Map<AuthorCreateResponseDto>(authorEntity))
            .Returns(expectedResponse);

        _validatorMock
            .Setup(v => v.ValidateAsync(authorEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repositoryMock
            .Setup(r => r.CreateAuthorAsync(authorEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(authorEntity.Id);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(generatedId);

        _mapperMock.Verify(m => m.Map<AuthorEntity>(requestDto), Times.Once);
        _validatorMock.Verify(v => v.ValidateAsync(authorEntity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.CreateAuthorAsync(authorEntity, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<AuthorCreateResponseDto>(authorEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidAuthor_ThrowsValidationException()
    {
        var invalidRequestDto = new AuthorCreateRequestDto(
            FirstName: "", 
            LastName: "Doe",
            CountryCode: "USA", 
            BirthDay: new DateOnly(2026, 1, 1));

        var command = new AuthorCreateCommand(invalidRequestDto);
        var authorEntity = new AuthorEntity();

        _mapperMock
            .Setup(m => m.Map<AuthorEntity>(invalidRequestDto))
            .Returns(authorEntity);

        var validationErrors = new List<ValidationFailure>
        {
            new("FirstName", "First name is required"),
            new("CountryCode", "Country code must be 2 characters"),
            new("BirthDay", "Author must be at least 10 years old")
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(authorEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationErrors));

        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _repositoryMock.Verify(r =>
            r.CreateAuthorAsync(It.IsAny<AuthorEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}

using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using LibraryService.Application.Commands.Author.Update;
using LibraryService.Application.Dto_s.Author.Update;
using LibraryService.Application.Dto_s.Author;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using Moq;

namespace LibraryService.Tests.Commands.Author;

public class AuthorUpdateCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<AuthorEntity>> _validatorMock;
    private readonly AuthorUpdateCommandHandler _handler;

    public AuthorUpdateCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<AuthorEntity>>();
        _handler = new AuthorUpdateCommandHandler(
            _repositoryMock.Object,
            _mapperMock.Object,
            _validatorMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_UpdatesAuthorAndReturnsId()
    {
        var authorId = Guid.NewGuid();
        var requestDto = new AuthorUpdateRequestDto(
            Id: authorId,
            FirstName: "Updated",
            LastName: "Name",
            CountryCode: "US",
            Books: new List<BookEntity>(),
            BirthDay: new DateOnly(1980, 1, 1));

        var command = new AuthorUpdateCommand(requestDto);
        var authorEntity = new AuthorEntity { Id = authorId };
        var updatedAuthorId = authorId; 

        _mapperMock
            .Setup(m => m.Map<AuthorEntity>(requestDto))
            .Returns(authorEntity);

        _mapperMock
            .Setup(m => m.Map<AuthorUpdateResponseDto>(authorEntity))
            .Returns(new AuthorUpdateResponseDto(authorId));

        _validatorMock
            .Setup(v => v.ValidateAsync(authorEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repositoryMock
            .Setup(r => r.UpdateAuthorAsync(authorEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedAuthorId);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(authorId);

        _mapperMock.Verify(m => m.Map<AuthorEntity>(requestDto), Times.Once);
        _validatorMock.Verify(v => v.ValidateAsync(authorEntity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAuthorAsync(authorEntity, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<AuthorUpdateResponseDto>(authorEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidData_ThrowsValidationException()
    {
        // Arrange
        var invalidRequestDto = new AuthorUpdateRequestDto(
            Id: Guid.NewGuid(),
            FirstName: "", 
            LastName: "Name",
            CountryCode: "USA",
            Books: new List<BookEntity>(),
            BirthDay: new DateOnly(2025, 1, 1));

        var command = new AuthorUpdateCommand(invalidRequestDto);
        var authorEntity = new AuthorEntity();

        _mapperMock
            .Setup(m => m.Map<AuthorEntity>(invalidRequestDto))
            .Returns(authorEntity);

        var validationErrors = new List<ValidationFailure>
        {
            new("FirstName", "First name is required"),
            new("CountryCode", "Invalid country code"),
            new("BirthDay", "Invalid date")
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(authorEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationErrors));

        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _repositoryMock.Verify(
            r => r.UpdateAuthorAsync(It.IsAny<AuthorEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}

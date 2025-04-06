using AutoMapper;
using LibraryService.Application.Commands.Author.Delete;
using LibraryService.Application.Dto_s.Author.Delete;
using LibraryService.Application.Dto_s.Author;
using LibraryService.Core.Contracts.Repositories;
using Moq;
using FluentAssertions;

namespace LibraryService.Tests.Commands.Author;

public class AuthorDeleteCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthorDeleteCommandHandler _handler;

    public AuthorDeleteCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new AuthorDeleteCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesAuthorAndReturnsEmptyResponse()
    {
        var authorId = Guid.NewGuid();
        var requestDto = new AuthorDeleteRequestDto(authorId);
        var command = new AuthorDeleteCommand(requestDto);

        _repositoryMock
            .Setup(r => r.DeleteAuthorAsync(authorId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<AuthorDeleteResponseDto>();

        _repositoryMock.Verify(
            r => r.DeleteAuthorAsync(authorId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_PropagatesException()
    {
        var authorId = Guid.NewGuid();
        var requestDto = new AuthorDeleteRequestDto(authorId);
        var command = new AuthorDeleteCommand(requestDto);

        _repositoryMock
            .Setup(r => r.DeleteAuthorAsync(authorId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("DB error"));

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
}

using FluentAssertions;
using LibraryService.Application.Queries.Author.GetById;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using Moq;

namespace LibraryService.Tests.Queries.Author;

public class AuthorGetByIdQueryHandlerTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly AuthorGetByIdQueryHandler _handler;

    public AuthorGetByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _handler = new AuthorGetByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsAuthor()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var query = new AuthorGetByIdQuery(authorId);

        var expectedAuthor = new AuthorEntity
        {
            Id = authorId,
            FirstName = "John",
            LastName = "Doe"
        };

        _repositoryMock
            .Setup(r => r.GetAuthorByIdAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAuthor);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedAuthor);
        _repositoryMock.Verify(r => r.GetAuthorByIdAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_AuthorNotFound_ReturnsNull()
    {
        var query = new AuthorGetByIdQuery(Guid.NewGuid());
        _repositoryMock
            .Setup(r => r.GetAuthorByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthorEntity)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_PropagatesException()
    {
        var query = new AuthorGetByIdQuery(Guid.NewGuid());
        _repositoryMock
            .Setup(r => r.GetAuthorByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("DB error"));

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }
}

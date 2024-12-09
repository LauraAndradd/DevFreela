using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsUserViewModel()
        {
            // Arrange
            var userId = 1;
            var user = new User("John Doe", "john@example.com", DateTime.Now, "password", "Client");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var query = new GetUserByIdQuery(userId);
            var handler = new GetUserByIdQueryHandler(userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(user.FullName, result.FullName); // Verifica se o nome está correto
            Assert.Equal(user.Email, result.Email); // Verifica se o email está correto

            userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once); // Verifica se o repositório foi chamado uma vez
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var userId = 999; // ID que não existe
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null); // Nenhum usuário encontrado

            var query = new GetUserByIdQuery(userId);
            var handler = new GetUserByIdQueryHandler(userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Null(result); // Verifica se o resultado é nulo quando o usuário não é encontrado
            userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once); // Verifica se o repositório foi chamado uma vez
        }
    }
}

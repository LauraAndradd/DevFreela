using DevFreela.Application.Commands.InsertUserCommand;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var createUserCommand = new CreateUserCommand
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                Password = "password123",
                Role = "user"
            };

            // Configura o hash da senha
            authServiceMock
                .Setup(a => a.ComputeSha256Hash(It.IsAny<string>()))
                .Returns("hashed_password");

            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            // Act
            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            // Assert
            Assert.True(id >= 0);
            userRepositoryMock.Verify(u => u.AddAsync(It.IsAny<User>()), Times.Once); // Verifica se AddAsync foi chamado
            authServiceMock.Verify(a => a.ComputeSha256Hash("password123"), Times.Once); // Verifica se a senha foi processada corretamente
        }
    }
}

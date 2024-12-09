using DevFreela.Application.Commands.LoginUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCredentials_ReturnsLoginUserViewModel()
        {
            // Arrange
            var email = "test@example.com";
            var password = "validPassword";
            var passwordHash = "hashedPassword";
            var user = new User("John Doe", "test@example.com", new DateTime(1990, 1, 1), passwordHash, "Admin");

            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.ComputeSha256Hash(password)).Returns(passwordHash);
            authServiceMock.Setup(service => service.GenerateJwtToken(email, "Admin")).Returns("jwtToken"); // Corrigido para "Admin"

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByEmailAndPasswordAsync(email, passwordHash)).ReturnsAsync(user);

            var loginUserCommand = new LoginUserCommand { Email = email, Password = password };
            var handler = new LoginUserCommandHandler(authServiceMock.Object, userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(loginUserCommand, new CancellationToken());

            // Assert
            Assert.NotNull(result);  // Verifica se o resultado não é nulo
            Assert.Equal(email, result.Email);  // Verifica se o email retornado é correto
            Assert.Equal("jwtToken", result.Token);  // Verifica se o token JWT gerado é o esperado
            userRepositoryMock.Verify(repo => repo.GetUserByEmailAndPasswordAsync(email, passwordHash), Times.Once); // Verifica se o repositório foi chamado uma vez
            authServiceMock.Verify(service => service.ComputeSha256Hash(password), Times.Once); // Verifica se a função de hash foi chamada
            authServiceMock.Verify(service => service.GenerateJwtToken(email, "Admin"), Times.Once); // Verifica se a geração do token foi chamada com o papel correto
        }

        [Fact]
        public async Task Handle_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var email = "test@example.com";
            var password = "invalidPassword";
            var passwordHash = "hashedPassword"; // Supondo que o serviço de hash irá gerar esse valor

            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.ComputeSha256Hash(password)).Returns(passwordHash);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByEmailAndPasswordAsync(email, passwordHash)).ReturnsAsync((User)null); // Nenhum usuário encontrado

            var loginUserCommand = new LoginUserCommand { Email = email, Password = password };
            var handler = new LoginUserCommandHandler(authServiceMock.Object, userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(loginUserCommand, new CancellationToken());

            // Assert
            Assert.Null(result); // Verifica se o resultado é nulo quando as credenciais são inválidas
            userRepositoryMock.Verify(repo => repo.GetUserByEmailAndPasswordAsync(email, passwordHash), Times.Once); // Verifica se o repositório foi chamado uma vez
        }
    }
}

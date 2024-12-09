using DevFreela.Application.Commands.InsertUserSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserSkillsCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CallsAddUserSkillsAsync()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(u => u.ExistsAsync(It.IsAny<int>())).ReturnsAsync(true);

            var createUserSkillsCommand = new CreateUserSkillsCommand
            {
                UserId = 1,
                SkillIds = new List<int> { 1, 2, 3 }
            };

            var handler = new CreateUserSkillsCommandHandler(userRepositoryMock.Object);

            // Act
            await handler.Handle(createUserSkillsCommand, new CancellationToken());

            // Assert
            userRepositoryMock.Verify(u => u.AddUserSkillsAsync(It.Is<List<UserSkill>>(list =>
                list.Count == 3 &&
                list.All(us => us.IdUser == createUserSkillsCommand.UserId && createUserSkillsCommand.SkillIds.Contains(us.IdUser))
            )), Times.Once);
        }
    }
}

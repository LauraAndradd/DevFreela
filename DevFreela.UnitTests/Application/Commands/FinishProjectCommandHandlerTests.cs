using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class FinishProjectCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CallsFinishAndSaveChangesAsync()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var project = new Project("Project Title", "Description", 1, 2, 1000);
            project.Start();  
            projectRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(project);

            var finishProjectCommand = new FinishProjectCommand(1);
            var handler = new FinishProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await handler.Handle(finishProjectCommand, new CancellationToken());

            // Assert
            projectRepositoryMock.Verify(repo => repo.GetById(It.Is<int>(id => id == 1)), Times.Once);  // Verifica se GetById foi chamado com o id correto
            projectRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);  // Verifica se SaveChangesAsync foi chamado uma vez
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);  // Verifica se o status do projeto foi alterado para 'Completed'
        }
    }
}

using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CallsStartAndUpdate()
        {
            // Arrange
            var projectId = 1;
            var project = new Project("Project Title", "Some Description", 1, 2, 1000); // Inicialize com dados válidos
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(repo => repo.GetById(projectId)).ReturnsAsync(project); // Mock para retornar o projeto
            projectRepositoryMock.Setup(repo => repo.Update(It.IsAny<Project>())).Returns(Task.CompletedTask); // Mock para simular atualização do projeto

            var startProjectCommand = new StartProjectCommand(projectId);
            var handler = new StartProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await handler.Handle(startProjectCommand, new CancellationToken());

            // Assert
            projectRepositoryMock.Verify(repo => repo.GetById(projectId), Times.Once); // Verifica se o projeto foi buscado uma vez
            projectRepositoryMock.Verify(repo => repo.Update(It.IsAny<Project>()), Times.Once); // Verifica se o método de atualização foi chamado
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status); // Verifica se o status do projeto foi alterado para "InProgress"
        }
    }
}

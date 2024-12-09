using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CallsUpdateAndUpdateRepository()
        {
            // Arrange
            var projectId = 1;
            var title = "Updated Project Title";
            var description = "Updated Project Description";
            var totalCost = 2000.00m;
            var project = new Project("Old Title", "Some Description", 1, 2, 1000); // Inicializa o projeto com dados antigos

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(repo => repo.GetById(projectId)).ReturnsAsync(project); // Mock para retornar o projeto
            projectRepositoryMock.Setup(repo => repo.Update(It.IsAny<Project>())).Returns(Task.CompletedTask); // Mock para simular a atualização do projeto

            var updateProjectCommand = new UpdateProjectCommand
            {
                IdProject = projectId,
                Title = title,
                Description = description,
                TotalCost = totalCost
            };
            var handler = new UpdateProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await handler.Handle(updateProjectCommand, new CancellationToken());

            // Assert
            projectRepositoryMock.Verify(repo => repo.GetById(projectId), Times.Once); // Verifica se o projeto foi buscado uma vez
            projectRepositoryMock.Verify(repo => repo.Update(It.Is<Project>(p =>
                p.Title == title &&
                p.Description == description &&
                p.TotalCost == totalCost)), Times.Once); // Verifica se o projeto foi atualizado com os novos dados

            Assert.Equal(title, project.Title); // Verifica se o título foi atualizado
            Assert.Equal(description, project.Description); // Verifica se a descrição foi atualizada
            Assert.Equal(totalCost, project.TotalCost); // Verifica se o custo total foi atualizado
        }
    }
}

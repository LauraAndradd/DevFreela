using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CallsCancelAndSaveChangesAsync()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var project = new Project("Project Title", "Project Description", 1, 1, 1000); 
            projectRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(project);

            var command = new DeleteProjectCommand(1);
            var handler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            projectRepositoryMock.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            projectRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);  
            Assert.True(project.IsDeleted); 
        }
    }
}

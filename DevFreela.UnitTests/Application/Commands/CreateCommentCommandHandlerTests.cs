using DevFreela.Application.Commands.InsertComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnCommentUnitValue()
        {
            //Arrange
            var projectRepository = new Mock<IProjectRepository>();

            var createCommentCommand = new CreateCommentCommand
            {
                Content = "Comentario de teste",
                IdProject = 15,
                IdUser = 11
            };

            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepository.Object);

            //Act
            var result = await createCommentCommandHandler.Handle(createCommentCommand, new CancellationToken());

            //Assert
            Assert.Equal(Unit.Value, result);
            projectRepository.Verify(pr => pr.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once);

        }
    }
}

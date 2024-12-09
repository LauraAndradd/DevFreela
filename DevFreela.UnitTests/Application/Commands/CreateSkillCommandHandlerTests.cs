using DevFreela.Application.Commands.InsertSkill;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateSkillCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnSkillUnitValue()
        {
            //Arrange
            var skillRepository = new Mock<ISkillRepository>();

            var createSkillCommand = new CreateSkillCommand("Test");

            var createSkillCommandHandler = new CreateSkillCommandHandler(skillRepository.Object);

            //Act
            var result = await createSkillCommandHandler.Handle(createSkillCommand, new CancellationToken());

            //Assert
            Assert.Equal(Unit.Value, result);
            skillRepository.Verify(pr => pr.Add(It.IsAny<Skill>()), Times.Once);
        }
    }
}

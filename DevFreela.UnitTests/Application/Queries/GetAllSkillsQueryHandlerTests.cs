using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ReturnsSkillViewModels()
        {
            // Arrange
            var skills = new List<Skill>
            {
                new Skill("C#"),
                new Skill("JavaScript")
            };

            var skillRepositoryMock = new Mock<ISkillRepository>();
            skillRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(skills); // Mock para retornar as habilidades

            var query = new GetAllSkillsQuery();
            var handler = new GetAllSkillsQueryHandler(skillRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(2, result.Count); // Verifica se o número de habilidades retornadas é 2
            Assert.Equal("C#", result[0].Description); // Verifica o nome da primeira habilidade
            Assert.Equal("JavaScript", result[1].Description); // Verifica o nome da segunda habilidade

            skillRepositoryMock.Verify(repo => repo.GetAll(), Times.Once); // Verifica se o repositório foi chamado uma vez
        }
    }
}

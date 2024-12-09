using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetProjectByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsProjectDetailsViewModel()
        {
            // Arrange
            var projectId = 1;
            var idClient = 2; // ID do cliente
            var idFreelancer = 3; // ID do freelancer
            var totalCost = 1000m; // Custo total

            var project = new Project("Project Title", "Project Description", idClient, idFreelancer, totalCost);

            var client = new User("Client Name", "client@example.com", DateTime.Now, "password", "Client");
            var freelancer = new User("Freelancer Name", "freelancer@example.com", DateTime.Now, "password", "Freelancer");

            // Atribuindo o cliente e freelancer diretamente
            project.SetClient(client); 
            project.SetFreelancer(freelancer);  

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(repo => repo.GetDetailsById(projectId)).ReturnsAsync(project);

            var query = new GetProjectByIdQuery(projectId);
            var handler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(project.Id, result.Id); // Verifica se o ID do projeto está correto
            Assert.Equal(project.Title, result.Title); // Verifica o título
            Assert.Equal(project.Description, result.Description); // Verifica a descrição
            Assert.Equal(project.TotalCost, result.TotalCost); // Verifica o custo total
            Assert.Equal(project.StartedAt, result.StartedAt); // Verifica a data de início
            Assert.Equal(project.FinishedAt, result.FinishedAt); // Verifica a data de término
            Assert.Equal(client.FullName, result.ClientFullName); // Verifica o nome do cliente
            Assert.Equal(freelancer.FullName, result.FreelancerFullName); // Verifica o nome do freelancer

            projectRepositoryMock.Verify(repo => repo.GetDetailsById(projectId), Times.Once); // Verifica se o repositório foi chamado uma vez
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var projectId = 999; // ID que não existe
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(repo => repo.GetDetailsById(projectId)).ReturnsAsync((Project)null); // Nenhum projeto encontrado

            var query = new GetProjectByIdQuery(projectId);
            var handler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Null(result); // Verifica se o resultado é nulo quando o projeto não é encontrado
            projectRepositoryMock.Verify(repo => repo.GetDetailsById(projectId), Times.Once); // Verifica se o repositório foi chamado uma vez
        }
    }
}

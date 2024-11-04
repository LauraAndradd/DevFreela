using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, int>
    {
        private readonly IProjectRepository _repository;
        public InsertProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _repository.Add(project);

            return project.Id;
        }
    }
}
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using System.Linq;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    internal class InsertUserSkillsHandler : IRequestHandler<InsertUserSkillsCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;

        public InsertUserSkillsHandler(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<ResultViewModel> Handle(InsertUserSkillsCommand command, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(command.UserId);

            if (!exists)
            {
                return ResultViewModel.Error("Projeto não existe.");
            }

            var userSkills = command.SkillIds
                    .Select(skillId => new UserSkill(command.UserId, skillId)) 
                    .ToList();

            await _repository.AddUserSkillsAsync(userSkills);

            return ResultViewModel.Success();
        }
    }
}

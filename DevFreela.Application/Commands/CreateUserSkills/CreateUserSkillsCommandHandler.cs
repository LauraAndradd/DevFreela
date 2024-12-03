using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using System.Linq;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    internal class CreateUserSkillsCommandHandler : IRequestHandler<CreateUserSkillsCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserSkillsCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserSkillsCommand command, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.ExistsAsync(command.UserId);

            var userSkills = command.SkillIds
                    .Select(skillId => new UserSkill(command.UserId, skillId)) 
                    .ToList();

            await _userRepository.AddUserSkillsAsync(userSkills);

            return Unit.Value;
        }
    }
}

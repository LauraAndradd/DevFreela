using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Unit>
    {
        private readonly ISkillRepository _skillRepository;

        public CreateSkillCommandHandler(ISkillRepository skillRepository) 
        {
            _skillRepository = skillRepository;
        }

        public async Task<Unit> Handle(CreateSkillCommand request, CancellationToken cancellationToken) 
        {
            Skill skill = new(request.Description);

            await _skillRepository.Add(skill);

            return Unit.Value;
        }
    }
}
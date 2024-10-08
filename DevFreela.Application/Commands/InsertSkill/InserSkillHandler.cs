using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InserSkillHandler : IRequestHandler<InserSkillCommand, ResultViewModel>
    {
        private readonly ISkillRepository _skillRepository;

        public InserSkillHandler(ISkillRepository skillRepository) 
        {
            _skillRepository = skillRepository;
        }

        public async Task<ResultViewModel> Handle(InserSkillCommand request, CancellationToken cancellationToken) 
        {
            Skill skill = new(request.Description);

            await _skillRepository.Add(skill);

            return ResultViewModel.Success();
        }
    }
}

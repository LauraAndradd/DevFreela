using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InserSkillCommand : IRequest<ResultViewModel>
    {
        public InserSkillCommand(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

        public Skill ToEntity()
            => new Skill(Description);
    }
}

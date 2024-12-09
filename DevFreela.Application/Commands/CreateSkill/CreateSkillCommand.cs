using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class CreateSkillCommand : IRequest<Unit>
    {
        public CreateSkillCommand(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

    }
}

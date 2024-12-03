using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    public class CreateUserSkillsCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public List<int> SkillIds { get; set; }
    }
}

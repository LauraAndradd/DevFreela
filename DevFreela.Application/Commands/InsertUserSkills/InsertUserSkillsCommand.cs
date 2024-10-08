using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    public class InsertUserSkillsCommand : IRequest<ResultViewModel>
    {
        public int UserId { get; set; }
        public List<int> SkillIds { get; set; }
    }
}

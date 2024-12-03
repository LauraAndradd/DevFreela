using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject
{
    public class FinishProjectCommand : IRequest<Unit>
    {
        public FinishProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

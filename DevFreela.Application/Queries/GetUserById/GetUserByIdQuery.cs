using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

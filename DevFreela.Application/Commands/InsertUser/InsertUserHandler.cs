using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserCommand
{
    public class InsertUserHandler
    {
        private readonly IUserRepository _userRepository;

        public InsertUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User(command.FullName, command.Email, command.BirthDate);

            await _userRepository.AddAsync(user);

            return Unit.Value;
        }
    }
}

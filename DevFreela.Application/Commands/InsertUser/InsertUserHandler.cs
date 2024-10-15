using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserCommand
{
    public class InsertUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public InsertUserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(command.Password);
            var user = new User(command.FullName, command.Email, command.BirthDate, passwordHash, command.Role);

            await _userRepository.AddAsync(user);

            return Unit.Value;
        }
    }
}

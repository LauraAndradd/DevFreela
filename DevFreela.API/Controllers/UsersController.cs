using DevFreela.Application.Commands.InsertUserCommand;
using DevFreela.Application.Commands.InsertUserSkills;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

            if (user is null)
              return NotFound();
           
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertUserSkillsCommand command)
        {
            command.UserId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Processar a imagem (futuro desenvolvimento)

            return Ok(description);
        }
    }
}

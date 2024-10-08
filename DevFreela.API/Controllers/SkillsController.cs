using DevFreela.Application.Commands.InsertSkill;
using DevFreela.Application.Queries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSkillsQuery());

            return Ok(result);
        }

        // POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(InserSkillCommand command)
        {
            var result = await _mediator.Send(new InserSkillCommand(command.Description));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}

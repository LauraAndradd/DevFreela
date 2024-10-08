using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly ISkillRepository _repository;
        public GetAllSkillsHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll();

            var model = projects.Select(SkillViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
    }
}

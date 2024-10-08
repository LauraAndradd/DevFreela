using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task Add(Skill skill);
        Task<List<Skill>> GetAll();
    }
}
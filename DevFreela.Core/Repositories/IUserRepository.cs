using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> AddAsync(User user);
        Task AddUserSkillsAsync(List<UserSkill> userSkills);
        Task<bool> ExistsAsync(int id);
        Task<User?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
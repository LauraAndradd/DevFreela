using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetDetailsById(int id);
        Task<Project?> GetById(int id);
        Task<int> AddAsync(Project project);
        Task Update(Project project);
        Task AddCommentAsync(ProjectComment comment);
        Task<bool> Exists(int id);
        Task SaveChangesAsync();
    }
}

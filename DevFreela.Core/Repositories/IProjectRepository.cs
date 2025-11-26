using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetById(int id);
        Task<Project> GetDetailById(int id);
        Task<int> Add(Project project);
        Task Update(Project project);
        Task Delete(int id);
        Task AddComent(ProjectComment comment);
        Task<bool> Exists(int id);


    }
}

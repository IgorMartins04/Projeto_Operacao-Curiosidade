using ProjetoForlogic.Models;

namespace ProjetoForlogic.Interfaces
{
    public interface ILogRepository
    {
        Task<List<LogView>> GetAllFromViewAsync();
        Task<List<LogView>> GetLogsByUserIdAsync(int userId);
        Task AddAsync(Log log);
    }
}

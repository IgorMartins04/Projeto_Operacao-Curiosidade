using ProjetoForlogic.Helpers;
using ProjetoForlogic.Models;

namespace ProjetoForlogic.Interfaces
{
    public interface ILogService
    {
        Task<ReturnOp<List<LogView>>> GetAllAsync(string userRole, int? userId = null);
        Task<ReturnOp<List<LogView>>> SearchLogsAsync(string text, string userRole, int? userId = null);
        Task<ReturnOp<Log>> CreateLogAsync(int? registerId, string action, int userId);
    }
}

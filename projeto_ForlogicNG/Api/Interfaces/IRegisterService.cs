using ProjetoForlogic.DTOs;
using ProjetoForlogic.Helpers;

namespace ProjetoForlogic.Interfaces
{
    public interface IRegisterService
    {
        Task<ReturnOp<List<RegisterDTO>>> GetAllAsync(int? userId = null);
        Task<ReturnOp<RegisterDTO>> GetByIdAsync(int id);
        Task<ReturnOp<int>> GetTotalAsync(int? userId = null);
        Task<ReturnOp<int>> GetLastMonthAsync(int? userId = null);
        Task<ReturnOp<int>> GetPendenciesAsync(int? userId = null);
        Task<ReturnOp<List<RegisterDTO>>> SearchRegistersAsync(string text, int? userId = null);
        Task<ReturnOp<RegisterDTO>> CreateRegisterAsync(RegisterCreatedDTO register, int userId);
        Task<ReturnOp<RegisterDTO>> UpdateRegisterAsync(int id, RegisterCreatedDTO registerEdited, int userId);
        Task<ReturnOp<bool>> RemoveRegisterAsync(int id);
        
    }
}

using ProjetoForlogic.Models;

namespace ProjetoForlogic.Interfaces
{
    public interface IRegisterRepository
    {
        Task<List<Register>> GetAllAsync();
        Task<Register?> GetByIdAsync(int id);
        Task<int> GetByStatusAsync(bool status, int? userId = null);
        Task AddAsync(Register register);
        Task UpdateAsync(Register register);
        Task RemoveAsync(Register register);
    }
}

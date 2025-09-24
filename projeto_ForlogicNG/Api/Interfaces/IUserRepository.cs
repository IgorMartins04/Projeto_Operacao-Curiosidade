using ProjetoForlogic.Models;

namespace ProjetoForlogic.Interfaces
{
    public interface IUserRepository
    {
        Task <List<User>> GetAllAsync();
        Task <User?> GetByIdAsync(int id);
        Task AddAsync(User user);
    }
}

using ProjetoForlogic.DTOs;
using ProjetoForlogic.Helpers;

namespace ProjetoForlogic.Interfaces
{
    public interface IUserService
    {
        Task<ReturnOp<List<UserDTO>>> GetUsersAsync();
        Task<ReturnOp<UserDTO>> GetByIdAsync(int id);
        Task<ReturnOp<UserDTO>> CreateUserAsync(UserCreatedDTO newUser);
    }
}

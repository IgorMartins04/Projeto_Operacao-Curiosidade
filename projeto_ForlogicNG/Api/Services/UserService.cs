using ProjetoForlogic.DTOs;
using ProjetoForlogic.Helpers;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Validations;

namespace ProjetoForlogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ReturnOp<List<UserDTO>>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDTO = users.Select(u => u.ToDTO()).ToList();
            return ReturnOp<List<UserDTO>>.Ok(usersDTO);
        }

        public async Task<ReturnOp<UserDTO>> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return ReturnOp<UserDTO>.Erro("Usuário não encontrado");

            return ReturnOp<UserDTO>.Ok(user.ToDTO());
        }

        public async Task<ReturnOp<UserDTO>> CreateUserAsync(UserCreatedDTO newUserDTO)
        {
            var validation = await UserValidations.ValidUserAsync(newUserDTO, _userRepository);
            if (!validation.Success)
                return ReturnOp<UserDTO>.Erro(validation.Message!);

            string hashedPassword = PasswordHelper.HashPassword(newUserDTO.Password);

            var newUser = newUserDTO.ToModel(hashedPassword);

            await _userRepository.AddAsync(newUser);

            return ReturnOp<UserDTO>.Ok(newUser.ToDTO());
        }
    }
}

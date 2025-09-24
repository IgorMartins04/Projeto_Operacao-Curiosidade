using ProjetoForlogic.Models;

namespace ProjetoForlogic.DTOs
{
    public static class ConvertUser
    {
        public static User ToModel(this UserCreatedDTO dto, string passwordHash)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash
            };
        }

        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}

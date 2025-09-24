using ProjetoForlogic.Models;

namespace ProjetoForlogic.DTOs
{
    public static class ConvertRegister
    {
        public static Register ToModel(this RegisterCreatedDTO dto, int userId)
        {
            return new Register
            {
                Name = dto.Name,
                Age = dto.Age,
                Email = dto.Email,
                Status = dto.Status,
                Address = dto.Address,
                Info = dto.Info,
                Interests = dto.Interests,
                Feelings = dto.Feelings,
                MValues = dto.MValues,
                DateRegister = DateTime.Now,
                UserId = userId,
            };
        }

        public static RegisterDTO ToDTO(this Register register)
        {
            return new RegisterDTO
            {
                Id = register.Id,
                Name = register.Name,
                Age = register.Age,
                Email = register.Email,
                Status = register.Status ? "Ativo" : "Inativo",
                Address = register.Address,
                Info = register.Info,
                Interests = register.Interests,
                Feelings = register.Feelings,
                MValues = register.MValues,
                DateRegister = register.DateRegister
            };
        }
    }
}

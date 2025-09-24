using ProjetoForlogic.DTOs;
using ProjetoForlogic.Helpers;
using ProjetoForlogic.Interfaces;
using System.Text.RegularExpressions;

namespace ProjetoForlogic.Validations
{
    public static class UserValidations
    {
        public static async Task<ReturnValidation> ValidUserAsync(UserCreatedDTO userDTO, IUserRepository userRepository)
        {
            var resultName = ValidName(userDTO.Name);
            if (!resultName.Success)
                return resultName;

            var resultEmail = await ValidEmailAsync(userDTO.Email, userRepository);
            if (!resultEmail.Success)
                return resultEmail;

            var resultPassword = ValidPassword(userDTO.Password);
            if (!resultPassword.Success)
                return resultPassword;

            return ReturnValidation.Ok();
        }

        public static ReturnValidation ValidName(string name)
        {
            if (Regex.IsMatch(name, @"\d"))
                return ReturnValidation.Erro("O nome não pode conter números");

            if (string.IsNullOrEmpty(name) || name.Length < 10 || name.Length > 30)
                return ReturnValidation.Erro("Nome inválido! Min 10 e max 30 caracteres");

            return ReturnValidation.Ok();
        }

        public static async Task<ReturnValidation> ValidEmailAsync(string email, IUserRepository userRepository)
        {
            var users = await userRepository.GetAllAsync();
            bool exists = users.Any(u => u.Email == email);
            if (exists)
                return ReturnValidation.Erro("Email já cadastrado!");

            if (string.IsNullOrWhiteSpace(email) || email.Length < 10 || email.Length > 70)
                return ReturnValidation.Erro("Email inválido! Min 10 e max 70 caracteres");

            var emailValid = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!emailValid.IsValid(email))
                return ReturnValidation.Erro("Email inválido");

            return ReturnValidation.Ok();
        }

        public static ReturnValidation ValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return ReturnValidation.Erro("Senha inválida! Min 8 caracteres");

            return ReturnValidation.Ok();
        }
    }
}

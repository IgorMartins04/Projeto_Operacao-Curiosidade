using ProjetoForlogic.DTOs;
using ProjetoForlogic.Helpers;
using ProjetoForlogic.Interfaces;
using System.Text.RegularExpressions;

namespace ProjetoForlogic.Validations
{
    public static class RegisterValidations
    {

        public static async Task<ReturnValidation> ValidRegisterAsync(RegisterCreatedDTO registerDTO, int? idCurrent,
            IRegisterRepository RegisterRepository)
        {
            var resultName = ValidName(registerDTO.Name);
            if (!resultName.Success)
                return resultName;

            var resultAge = ValidAge(registerDTO.Age);
            if (!resultAge.Success)
                return resultAge;
            
            var resultEmail = await ValidEmailAsync(registerDTO.Email, idCurrent, RegisterRepository);
            if (!resultEmail.Success)
                return resultEmail;

            var resultAddress = ValidAddress(registerDTO.Address);
            if (!resultAddress.Success)
                return resultAddress;

            var resultOptional = ValidOptional(registerDTO.Info, registerDTO.Interests, 
                            registerDTO.Feelings, registerDTO.MValues);
            if (!resultOptional.Success)
                return resultOptional;

            return ReturnValidation.Ok();
        }

        public static ReturnValidation ValidName(string name)
        {
            if (Regex.IsMatch(name, @"\d"))
                return ReturnValidation.Erro("O nome não pode conter números");
            
            if(string.IsNullOrEmpty(name) || name.Length < 10)
                return ReturnValidation.Erro("Nome inválido (mínimo 10 caracteres)");

            if (name.Length > 30)
                return ReturnValidation.Erro("Nome inválido (máximo 30 caracteres)");

            return ReturnValidation.Ok();
        }

        public static async Task<ReturnValidation> ValidEmailAsync(string email, int? idCurrent, 
            IRegisterRepository registerRepository)
        {
            var registers = await registerRepository.GetAllAsync();
            bool exists = registers.Any(r => r.Email == email && r.Id != idCurrent);

            if (exists)
                return ReturnValidation.Erro("Email já cadastrado!");

            if (string.IsNullOrEmpty(email) || email.Length < 10 || email.Length > 70)
                return ReturnValidation.Erro("Email inválido, min de 10 e max 70 caracteres!");

            var emailValid = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!emailValid.IsValid(email))
                return ReturnValidation.Erro("Email inválido");

            return ReturnValidation.Ok();
        }

        public static ReturnValidation ValidAge(int age)
        {
            if(age < 18 || age > 99)
                return ReturnValidation.Erro("A idade precisa estar entre 18 e 99");

            return ReturnValidation.Ok();
        }

        public static ReturnValidation ValidAddress(string address)
        {
            if (string.IsNullOrEmpty(address) || address.Length > 70)
                return ReturnValidation.Erro("Endereco invalido! max 70 caracteres");

            return ReturnValidation.Ok();
        }

        public static ReturnValidation ValidOptional(string? text1, string? text2,
                                                        string? text3 , string? text4)
        {

            if ((text1?.Length ?? 0) > 150 ||
                (text2?.Length ?? 0) > 150 ||
                (text3?.Length ?? 0) > 150 ||
                (text4?.Length ?? 0) > 150)
            {
                return ReturnValidation.Erro("Tamanho invalido! Limite máximo de 150 caracteres");
            }

            return ReturnValidation.Ok();

        }
    }
}

using ProjetoForlogic.DataBase;
using ProjetoForlogic.DTOs;
using ProjetoForlogic.Helpers;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Validations;

namespace ProjetoForlogic.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;
        private readonly AppDbContext _context;
        public RegisterService(IRegisterRepository registerRepository, AppDbContext context)
        {
            _registerRepository = registerRepository;
            _context = context; 
        }
        public async Task<ReturnOp<List<RegisterDTO>>> GetAllAsync(int? userId = null)
        {
            var registers = await _registerRepository.GetAllAsync();
            if (userId.HasValue)
            {
                registers = registers.Where(r => r.UserId == userId.Value).ToList();
            }
            var registersDTO = registers.Select(r => r.ToDTO()).ToList();
            return ReturnOp<List<RegisterDTO>>.Ok(registersDTO);
        }

        public async Task<ReturnOp<RegisterDTO>> GetByIdAsync(int id)
        {
            var register = await _registerRepository.GetByIdAsync(id);
            if (register == null)
                return ReturnOp<RegisterDTO>.Erro("Cadastro não encontrado");

            return ReturnOp<RegisterDTO>.Ok(register.ToDTO());
        }

        public async Task<ReturnOp<int>> GetTotalAsync(int? userId = null)
        {
            var registers = await _registerRepository.GetAllAsync();
            if (userId.HasValue)
            {
                registers = registers.Where(r => r.UserId == userId.Value).ToList();
            }
            var total = registers.Count();
            return ReturnOp<int>.Ok(total);
        }

        public async Task<ReturnOp<int>> GetLastMonthAsync(int? userId = null)
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var registers = await _registerRepository.GetAllAsync();
            if (userId.HasValue)
            {
                registers = registers.Where(r => r.UserId == userId.Value).ToList();
            }
            var total = registers.Count(r => r.DateRegister >= lastMonth);

            return ReturnOp<int>.Ok(total);
        }

        public async Task<ReturnOp<int>> GetPendenciesAsync(int? userId = null)
        {
            var registers = await _registerRepository.GetByStatusAsync(false, userId);
            return ReturnOp<int>.Ok(registers);
        }

        public async Task<ReturnOp<List<RegisterDTO>>> SearchRegistersAsync(string text, int? userId = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                return await GetAllAsync();

            var registers = await _registerRepository.GetAllAsync();
            var textLower = text.ToLower();

            var registersFiltered = registers
                .Where(r =>
                    r.Name.ToLower().Contains(textLower) ||
                    r.Email.ToLower().Contains(textLower) ||
                   (r.Status ? "ativo" : "inativo").Contains(textLower)
                )
                .ToList();
            if (userId.HasValue)
            {
                registersFiltered = registersFiltered.Where(r => r.UserId == userId.Value).ToList();
            }

            var listDTO = registersFiltered.Select(r => r.ToDTO()).ToList();
            return ReturnOp<List<RegisterDTO>>.Ok(listDTO);
        }
        public async Task<ReturnOp<RegisterDTO>> CreateRegisterAsync(RegisterCreatedDTO newRegisterDTO, int userId)
        {
            var validation = await RegisterValidations.ValidRegisterAsync(newRegisterDTO, null, _registerRepository);
            if (!validation.Success)
                return ReturnOp<RegisterDTO>.Erro(validation.Message!);

            var newRegister = newRegisterDTO.ToModel(userId);

            await _registerRepository.AddAsync(newRegister);

            return ReturnOp<RegisterDTO>.Ok(newRegister.ToDTO());
        }

        public async Task<ReturnOp<RegisterDTO>> UpdateRegisterAsync(int id, RegisterCreatedDTO registerEditedDTO, int userId)
        {
            var register = await _registerRepository.GetByIdAsync(id);
            if (register == null)
                return ReturnOp<RegisterDTO>.Erro("Cadastro não encontrado");

            var validation = await RegisterValidations.ValidRegisterAsync(registerEditedDTO, id, 
                _registerRepository);
            if (!validation.Success)
                return ReturnOp<RegisterDTO>.Erro(validation.Message!);

            var registerEdited = registerEditedDTO.ToModel(userId);

            register.Status = registerEdited.Status;
            register.Name = registerEdited.Name;
            register.Age = registerEdited.Age;
            register.Email = registerEdited.Email;
            register.Address = registerEdited.Address;
            register.Info = registerEdited.Info;
            register.Interests = registerEdited.Interests;
            register.Feelings = registerEdited.Feelings;
            register.MValues = registerEdited.MValues;

            await _registerRepository.UpdateAsync(register);

            return ReturnOp<RegisterDTO>.Ok(register.ToDTO());
        }

        public async Task<ReturnOp<bool>> RemoveRegisterAsync(int id)
        {
            var register = await _registerRepository.GetByIdAsync(id);
            if (register == null)
                return ReturnOp<bool>.Erro("Cadastro não encontrado");

            await _registerRepository.RemoveAsync(register);
            return ReturnOp<bool>.Ok(true);
        }
    }
}

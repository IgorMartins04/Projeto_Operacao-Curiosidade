using Microsoft.EntityFrameworkCore;
using ProjetoForlogic.DataBase;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Models;

namespace ProjetoForlogic.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly AppDbContext _context;
        public RegisterRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Register>> GetAllAsync()
        {
            return await _context.Registers.AsNoTracking().
                Where(r => !r.Removed).ToListAsync();
        }

        public async Task<Register?> GetByIdAsync(int id)
        {
            return await _context.Registers
                .AsNoTracking()
                .Where(r => !r.Removed && r.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<int> GetByStatusAsync(bool status, int? userId = null)
        {
            var result = await _context.Set<InactiveCount>()
                .FromSqlRaw("EXEC usp_ListRegistersByStatus @Status = {0}, @UserId = {1}", status, userId)
                .ToListAsync();

            return result.FirstOrDefault()?.Total ?? 0;
        }
        public async Task AddAsync(Register register)
        {
            _context.Registers.Add(register);
            await _context.SaveChangesAsync(); 
        }

        public async Task UpdateAsync(Register register)
        {
            var existingRegister = await _context.Registers
                .FirstOrDefaultAsync(r => !r.Removed && r.Id == register.Id);

            if (existingRegister == null)
                throw new Exception("Registro não encontrado");

            existingRegister.Name = register.Name;
            existingRegister.Email = register.Email;
            existingRegister.Age = register.Age;
            existingRegister.Address = register.Address;
            existingRegister.Status = register.Status;
            existingRegister.Info = register.Info;
            existingRegister.Interests = register.Interests;
            existingRegister.Feelings = register.Feelings;
            existingRegister.MValues = register.MValues;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Register register)
        {
            var existsRegister = await _context.Registers
                .FirstOrDefaultAsync(r => r.Id == register.Id);

            if (existsRegister != null)
            {
                existsRegister.Removed = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}

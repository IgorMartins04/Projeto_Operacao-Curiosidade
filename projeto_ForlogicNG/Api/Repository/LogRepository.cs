using ProjetoForlogic.DataBase;
using Microsoft.EntityFrameworkCore;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Models;

namespace ProjetoForlogic.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly AppDbContext _context;
        public LogRepository(AppDbContext context)
        {
           _context = context;
        }
        public async Task<List<LogView>> GetAllFromViewAsync()
        {
            return await _context.LogsView
                .FromSqlRaw("SELECT * FROM vw_Logs") 
                .ToListAsync();
        }
        public async Task<List<LogView>> GetLogsByUserIdAsync(int userId)
        {
            return await _context.Set<LogView>()
                .FromSqlRaw("EXEC usp_GetLogsByUserId @UserId = {0}", userId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task AddAsync(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}

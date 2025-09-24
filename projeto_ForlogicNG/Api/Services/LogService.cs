using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ProjetoForlogic.Helpers;
using ProjetoForlogic.Interfaces;
using ProjetoForlogic.Models;

namespace ProjetoForlogic.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public LogService(ILogRepository logRepository, IUserRepository userRepository)
        {
            _logRepository = logRepository;
            _userRepository = userRepository;
        }
        public async Task<ReturnOp<List<LogView>>> GetAllAsync(string userRole, int? userId = null)
        {
            List<LogView> logs;

            if (userRole == "Master")
            {
                logs = await _logRepository.GetAllFromViewAsync();
            }
            else
            {
                if (!userId.HasValue)
                    return ReturnOp<List<LogView>>.Erro("Usuário não identificado.");

                logs = await _logRepository.GetLogsByUserIdAsync(userId.Value);
            }

            logs = logs.OrderByDescending(l => l.DateRegister).ToList();
            return ReturnOp<List<LogView>>.Ok(logs);
        }

        public async Task<ReturnOp<List<LogView>>> SearchLogsAsync(string text, string userRole, int? userId = null)
        {
            List<LogView> logs;

            if (userRole == "Master")
            {
                logs = await _logRepository.GetAllFromViewAsync();
            }
            else
            {
                if (!userId.HasValue)
                    return ReturnOp<List<LogView>>.Erro("Usuário não identificado.");

                logs = await _logRepository.GetLogsByUserIdAsync(userId.Value);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                var textLower = text.ToLower();
                logs = logs
                    .Where(l =>
                        l.UserName.ToLower().Contains(textLower) ||
                        l.Email.ToLower().Contains(textLower) ||
                        l.Action.ToLower().Contains(textLower))
                    .ToList();
            }

            logs = logs.OrderByDescending(l => l.DateRegister).ToList();
            return ReturnOp<List<LogView>>.Ok(logs);
        }

        public async Task<ReturnOp<Log>> CreateLogAsync(int? registerId, string action, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return ReturnOp<Log>.Erro("Usuário não encontrado.");

            var newLog = new Log
            {
                RegisterId = registerId,
                UserId = userId,
                UserName = user.Name,
                Email = user.Email,
                Action = action,
                DateRegister = DateTime.Now
            };

            await _logRepository.AddAsync(newLog);

            return ReturnOp<Log>.Ok(newLog);
        }

    }
}

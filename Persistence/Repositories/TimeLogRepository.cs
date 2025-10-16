using Application.Repositories.TimeLogRepo;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TimeLogRepository : RepositoryBase<TimeLog>, ITimeLogRepository
    {
        public TimeLogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TimeLog?> GetLastestLogByEmpIdAsync(int employeeId)
        {
            return await _dbContext.TimeLogs
                .Where(t => t.EmployeeId == employeeId)
                .OrderByDescending(t => t.LogTime)//En son log kaydını almak için LogTime'a göre azalan sırala
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<TimeLog?> GetLastestLogByTypeAsync(int employeeId, LogType logType)
        {
            return await _dbContext.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.LogType == logType)
                .OrderByDescending(t => t.LogTime)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TimeLog>> GetLogsByEmpAndDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.LogTime >= startDate && t.
                LogTime <= endDate.Date.AddDays(1)) //Gün sonunu dahil etmek için
                .OrderByDescending(t => t.LogTime)
                .AsNoTracking()
                .ToListAsync();


        }

        public async Task<IReadOnlyList<TimeLog>> GetLogsForToday(int employeeId)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _dbContext.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.LogTime >= today && t.LogTime < tomorrow)
                .OrderBy(t => t.LogTime) //Kronolojik sıraya göre
                .AsNoTracking()
                .ToListAsync();

        }
    }
}

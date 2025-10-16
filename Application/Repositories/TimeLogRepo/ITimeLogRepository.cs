using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.TimeLogRepo
{
    public interface ITimeLogRepository : IAsyncRepository<TimeLog>
    {
        //Çalışanın bugüne ait tüm kayıtlarını getirir.
        Task<IReadOnlyList<TimeLog>> GetLogsForToday(int employeeId);

        //Raporlama için tarih aralığı ve çalışan ID'sine göre listeleme
        Task<IReadOnlyList<TimeLog>> GetLogsByEmpAndDateRangeAsync(
            int employeeId,
            DateTime startDate,
            DateTime endDate
            );

        // Giriş-Çıkış için en son Log'u getir
        Task<TimeLog> GetLastestLogByEmpIdAsync(int employeeId);

        //Spesifik bir duruma göre en son logu getir
        Task<TimeLog> GetLastestLogByTypeAsync(int employeeId, LogType logType);
    }
}

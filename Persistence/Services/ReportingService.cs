using Application.Abstract;
using Application.Models.Reports;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ReportingService : IReportingService
    {
        private readonly ApplicationDbContext _dbContext;
        public Task<IReadOnlyList<ReportResultDto>> GetMonthlyEmployeeReport(int year, int month)
        {
            throw new NotImplementedException();
        }
    }
}

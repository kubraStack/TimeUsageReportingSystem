using Application.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IReportingService
    {
        //Yıl ve ay parametrelerini gelir ve DTO listesini (Rapor sonucunu) döndürür.
        Task<IReadOnlyList<ReportResultDto>> GetMonthlyEmployeeReport(int year, int month);
    }
}

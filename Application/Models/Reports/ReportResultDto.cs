using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Reports
{
    public class ReportResultDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double TotalWorkHours { get; set; }
        public int TotalRefectoryUsage { get; set; }
        public int LateEntryCount { get; set; }
    }
}

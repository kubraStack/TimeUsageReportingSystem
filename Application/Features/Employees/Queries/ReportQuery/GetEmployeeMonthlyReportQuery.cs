using Application.Models.Reports;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.ReportQuery
{
    public class GetEmployeeMonthlyReportQuery : IRequest<List<ReportResultDto>>, ISecuredRequest
    {
        //Raporun alınacağı başlangıç ve bitiş tarihleri
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string[] RequiredRoles => new[] {"Admin"};
    }
}

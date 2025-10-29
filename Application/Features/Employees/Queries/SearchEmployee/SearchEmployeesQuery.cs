using Application.Features.Employees.Models;
using Application.Models.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.SearchEmployee
{
    public class SearchEmployeesQuery : IRequest<EmployeeListDto>
    {
        public string? SearchTerm { get; set; }
        public PageRequest PageRequest { get; set; } = new PageRequest { Page = 0, PageSize = 10 };
    }
}

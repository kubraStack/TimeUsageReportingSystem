using Application.Features.Employees.Models;
using Application.Models.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAllEmployee
{
    public class EmployeeListDto : ListModel<EmployeeDetailDto>
    {
    }
    public class GetEmployeeListQuery : IRequest<EmployeeListDto>
    {
        public PageRequest PageRequest { get; set; } = new PageRequest { Page=0, PageSize=10};
    }
}

using Application.Features.Department.Models;
using Application.Models.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries.GetAllDepartment
{
    public class GetAllDepartmentsQuery : IRequest<DepartmentListDto>
    {
        public PageRequest  PageRequest { get; set; } = new PageRequest { Page=0, PageSize=10};
    }
}

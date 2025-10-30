using Application.Features.Department.Models;
using Application.Models.Paging;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries.GetAllDepartmentsForByAdmin
{
    public class GetAllDepartmentForByAdminQuery : IRequest<DepartmentListDto>, ISequredRequest
    {
        public PageRequest PageRequest { get; set; } = new PageRequest { Page = 0, PageSize = 10 };

        public string[] RequiredRoles => new[] { "Admin" };
    }
}

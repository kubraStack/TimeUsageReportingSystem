using Application.Features.Department.Models;
using Application.Repositories.DepartmentRepo;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries.GetAllDepartmentsForByAdmin
{
    public class GetAllDepartmentForByAdminHandler : IRequestHandler<GetAllDepartmentForByAdminQuery, DepartmentListDto> 
    {
       
        public Task<DepartmentListDto> Handle(GetAllDepartmentForByAdminQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

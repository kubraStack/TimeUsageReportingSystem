using Application.Features.Department.Models;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQuery : IRequest<DepartmentDetailDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] RequiredRoles => new[] { "Admin","HR","IK"};
    }
}

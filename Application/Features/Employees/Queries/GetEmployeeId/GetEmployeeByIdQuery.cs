using Application.Features.Employees.Models;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetEmployeeId
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDetailDto>, ISecuredRequest
    {
        public int UserIdFromToken { get; set; }

        public string[] RequiredRoles => new[] {"Admin","Manager","Employee" };
    }
}

using Application.Features.Employees.Command.Update;
using Core.Application.Pipelines.Authorization;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdminManager.Commands.UpdateEmployee
{
    public class UpdateEmployeeForAdminCommand : IRequest<UpdateEmployeeForAdminResponse>, ISecuredRequest
    {
        public int EmployeeId { get; set; }
        public int NewDepartmentId { get; set; }
        public UserType  NewRole { get; set; }
        public string[] RequiredRoles => new[] {"Admin","Manager" };
    }
}

using Core.Application.Pipelines.Authorization;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.Update
{
    public class UpdateEmployeeCommand : IRequest<UpdateEmployeeResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
     

        public string[] RequiredRoles => new[] { "Admin","Manager","Employee"};
    }
}

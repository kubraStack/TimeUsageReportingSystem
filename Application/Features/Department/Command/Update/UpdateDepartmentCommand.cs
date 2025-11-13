using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Command.Update
{
    public class UpdateDepartmentCommand : IRequest<UpdateDepartmentResponse>, ISecuredRequest
    {
        public int Id { get; set; } //Hangi kaydın güncelleneceği
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] RequiredRoles => new[] { "Admin","Manager"};
    }
}

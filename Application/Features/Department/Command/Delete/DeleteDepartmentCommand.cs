using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Command.Delete
{
    public class DeleteDepartmentCommand : IRequest<DeleteDepartmentResponse>
    {
        public int Id { get; set; }
    }
}

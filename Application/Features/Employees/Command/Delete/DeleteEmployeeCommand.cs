using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.Delete
{
    public class DeleteEmployeeCommand : IRequest<DeleteEmployeeResponse>
    {
        public int Id { get; set; }
    }
}

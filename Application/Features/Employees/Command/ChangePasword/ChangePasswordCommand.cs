using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.ChangePasword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
    {
        public int EmployeeId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

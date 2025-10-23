using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.ChangePasword
{
    public class ChangePasswordResponse
    {
        public int EmployeeId { get; set; }
        public bool Success { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Message { get; set; }
    }
}

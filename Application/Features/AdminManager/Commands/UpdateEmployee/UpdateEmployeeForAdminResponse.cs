using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdminManager.Commands.UpdateEmployee
{
    public class UpdateEmployeeForAdminResponse
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public UserType  Role { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : BaseUser
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;


        public ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
        public ICollection<UserOperationClaim> UserOperationClaims { get; set; } = new List<UserOperationClaim>();
    }
}

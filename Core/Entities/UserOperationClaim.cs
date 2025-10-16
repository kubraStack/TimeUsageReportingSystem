using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserOperationClaim : Entity
    {
        public int EmployeeId { get; set; }
        public int OperationClaimId { get; set; }

        public OperationClaim OperationClaims { get; set; } = null!;
    }
}

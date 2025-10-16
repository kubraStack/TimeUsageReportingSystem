using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OperationClaim : Entity
    {
        public string Name { get; set; }
        public ICollection<UserOperationClaim> UserOperationClaim { get; set; } = new List<UserOperationClaim>();
    }
}

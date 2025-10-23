using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.Delete
{
    public class DeleteEmployeeResponse
    {
        public int Id { get; set; }
        public DateTime DeletedTime { get; set; }
        public string Message { get; set; }
    }
}

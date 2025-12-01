using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.CheckIn
{
    public class CheckInCommandResponse
    {
        public int Id { get; set; }
        public DateTime CheckInTime { get; set; }
        public string EmployeeEmail { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TimeLog : Entity
    {
        public DateTime LogTime { get; set; }
        public LogType LogType { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;


    }
}

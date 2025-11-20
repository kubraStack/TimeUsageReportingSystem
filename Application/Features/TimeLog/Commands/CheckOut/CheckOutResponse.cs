using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.CheckOut
{
    public class CheckOutResponse
    {
        public int Id { get; set; }
        public DateTime ChechInTime { get; set; }
        public DateTime ChechOutTime { get; set; }
        public TimeSpan Duration { get; set; } //Çalışma süresi
        public string EmployeeEmail { get; set; }
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.Update
{
    public class UpdateTimeLogResponse
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public LogType LogType { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

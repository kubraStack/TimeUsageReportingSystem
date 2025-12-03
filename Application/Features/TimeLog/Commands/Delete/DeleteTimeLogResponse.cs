using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.Delete
{
    public class DeleteTimeLogResponse
    {
        public int Id { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}

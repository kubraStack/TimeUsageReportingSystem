using Core.Application.Pipelines.Chaching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.Update
{
    public class UpdateTimeLogCommand : IRequest<UpdateTimeLogResponse>, ITransactionRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } //Değişiklik yapılırsa hangi çalışana ait olduğu güncellenebilir
        public DateTime LogTime { get; set; }
        public LogType  LogType { get; set; } 
        public string CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "TimeLogList";
    }
}

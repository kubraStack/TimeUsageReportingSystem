using Core.Application.Pipelines.Chaching;
using Core.Application.Pipelines.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.CheckIn
{
    public class CheckInCommand : IRequest<CheckInCommandResponse>, ITransactionRequest, ICacheRemoverRequest
    {
        public int EmployeeId { get; set; }
        public string CacheKey =>"";

        public bool BypassCache => false;

        public string? CacheGroupKey => "TimeLogList";
    }
}

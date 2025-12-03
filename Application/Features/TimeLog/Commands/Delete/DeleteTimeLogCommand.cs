using Core.Application.Pipelines.Chaching;
using Core.Application.Pipelines.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.Delete
{
    public class DeleteTimeLogCommand : IRequest<DeleteTimeLogResponse>, ITransactionRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }

        public string CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "TimeLogList";
    }
}

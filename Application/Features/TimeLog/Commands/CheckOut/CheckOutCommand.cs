using Core.Application.Pipelines.Chaching;
using Core.Application.Pipelines.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.CheckOut
{
    public class CheckOutCommand : IRequest<CheckOutResponse>, ITransactionRequest, ICacheRemoverRequest
    {
        public int EmployeeId { get; set; }
        public string CacheKey => ""; // Belirli bir anahtar temizlenmeyecek

        public bool BypassCache => false;
        //Timelog Listesi değiştiği için liste cache'ini temizle
        public string? CacheGroupKey => "TimeLogList";
    }
}

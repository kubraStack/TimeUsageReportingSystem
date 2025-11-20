using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionRequest
    {
        private readonly ITransactionService _transactionService;

        public TransactionBehavior(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Transaction başlat
            await _transactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                // Command'ı çalıştır
                TResponse response = await next();

                // Başarılıysa onay (Commit)
                await _transactionService.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                // Hata varsa geri al (Rollback)
                await _transactionService.RollbackTransactionAsync(cancellationToken);
                throw ex; // Hatayı tekrar fırlat
            }
        }
    }
}

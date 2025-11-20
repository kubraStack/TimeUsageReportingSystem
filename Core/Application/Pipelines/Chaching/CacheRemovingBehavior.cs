using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Chaching
{
    public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>, ICacheRemoverRequest
    {
        private readonly ICacheService _cacheService;

        public CacheRemovingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Commandı çalıştırmadan önce önbellek temizleme yapılırsa (BypassCache = true ise atlanır)

            if (request.BypassCache)
            {
             return await next();   
            }

            //Commandı çalıştır
            TResponse response = await next();

            //Başarılı olursa önbelleği temizle
            if (request.CacheGroupKey != null)
            {
                //Gruba ait tüm anahtarları temizle
                await _cacheService.RemoveCacheGroupAsync(request.CacheGroupKey);
            }
            else if (request.CacheKey != null) 
            {
                //Belirli bir anahtarı temizle
                await _cacheService.RemoveAsync(request.CacheKey);            
            }

            return response;
        }
    }
}

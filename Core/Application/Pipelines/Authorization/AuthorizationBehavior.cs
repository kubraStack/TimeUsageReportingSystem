using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>, ISequredRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new BusinessException("Giriş Yapmalısınız");
            }

            if (request.RequiredRoles.Any())
            {
                ICollection<string>? userRoles = _httpContextAccessor.HttpContext.User.Claims
                    .Where(i => i.Type == ClaimTypes.Role)
                    .Select(i => i.Value)
                    .ToList();

                bool hasNoMatchingRole = string.IsNullOrEmpty(
                    userRoles.FirstOrDefault(i => i == "Admin" || request.RequiredRoles.Contains(i))
                );

                if (hasNoMatchingRole)
                {
                    throw new BusinessException("Bu İşlemi Gerçekleştirme Yetkiniz Yok !");
                }
               
            }
            TResponse response = await next();
            return response;
        }
    }
}

using Core.Application.Pipelines.Chaching;
using Core.Application.Pipelines.Transaction;
using Core.Utilities.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, TokenOptions tokenOptions)
        {
            services.AddScoped<ITokenHelper, JwtHelper>(_ => new JwtHelper(tokenOptions));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            services.AddTransient(
                            typeof(IPipelineBehavior<,>),
                            typeof(TransactionBehavior<,>) // ⬅️ KESİN ÇÖZÜM
            );
            return services;
        }
    }
}

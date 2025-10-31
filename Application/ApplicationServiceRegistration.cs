using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using Application.Features.Auth.Register;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //automapper 
            services.AddMediatR(cfg =>
            {
                //komutlar ve sorgular application katmanında aransın
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                //LoggingBehavior eklenebilir buraya
                cfg.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                
            });
            //mediatr
          
            return services;
        }
    }
}

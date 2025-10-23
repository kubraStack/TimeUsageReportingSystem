using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            });
            //mediatr
          
            return services;
        }
    }
}

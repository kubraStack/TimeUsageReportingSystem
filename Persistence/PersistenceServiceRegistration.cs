using Application.Repositories.DepartmentRepo;
using Application.Repositories.EmployeeRepo;
using Application.Repositories.TimeLogRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
            );

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITimeLogRepository, TimeLogRepository>();
            services.AddScoped<IDepartmentRepository,  DepartmentRepository>();

            return services;
        }
    }
}

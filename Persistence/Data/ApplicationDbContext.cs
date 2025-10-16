using Application.Models.Reports;
using Core.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Domain Varlıkları

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<TimeLog> TimeLogs { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;

        //Güvenlik varlıkları
        public DbSet<OperationClaim> OperationClaims { get; set; } = null!;
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<ReportResultDto>().HasNoKey(); //Raporlama DTO'su anahtarsız

            //SoftDelete için global query filtresi
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    //e parametresi o an ki varlık türünü temsil eder
                    var parameter = Expression.Parameter(entityType.ClrType, "e");

                    //e.IsDeleted özelliğini temsil eder
                    var propertyMethod = typeof(EF).GetMethod("Property")
                        .MakeGenericMethod(typeof(bool));

                    var isDeletedProperty = Expression.Call(

                        propertyMethod,
                        parameter,
                        Expression.Constant("IsDeleted")

                    );

                    //e.IsDeleted == false ifadesini oluşturur
                    var filter = Expression.Equal(

                        isDeletedProperty,
                        Expression.Constant(false)
                    );

                    //Lambda oluşturma e=> e.IsDeleted == false
                    var lambda = Expression.Lambda(filter, parameter);

                    //Global Filtre
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        //SaveChanges ovveride metodu(soft delete mantığı için)
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity entity && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    entity.DeletedDate = DateTime.UtcNow;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

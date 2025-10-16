using Core.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims").HasKey(uoc => uoc.Id);

            //Employee İlişkisi
            builder.HasOne<Employee>()
                .WithMany(e => e.UserOperationClaims)
                .HasForeignKey(uoc => uoc.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            //OperationClaim İlişkisi
            builder.HasOne(uoc => uoc.OperationClaims)
                .WithMany(oc => oc.UserOperationClaim)
                .HasForeignKey(uoc => uoc.OperationClaimId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

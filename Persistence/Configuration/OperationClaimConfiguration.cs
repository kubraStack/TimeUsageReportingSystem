using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("OperationClaims");
            builder.HasKey(oc => oc.Id);

            builder.Property(oc => oc.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(oc => oc.Name).IsUnique();
        }
    }
}

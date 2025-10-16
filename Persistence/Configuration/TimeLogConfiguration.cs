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
    public class TimeLogConfiguration : IEntityTypeConfiguration<TimeLog>
    {
        public void Configure(EntityTypeBuilder<TimeLog> builder)
        {
            builder.ToTable("TimeLogs");
            builder.HasKey(tl => tl.Id);

            // Kısıtlamalar
            builder.Property(tl => tl.LogTime).IsRequired();

            //İlişkiler
            builder.HasOne(tl => tl.Employee)
                   .WithMany(u => u.TimeLogs)
                   .HasForeignKey(tl => tl.EmployeeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

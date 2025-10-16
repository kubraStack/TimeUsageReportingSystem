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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);

            //Kısaltmalar
            builder.Property(e => e.EncryptedFirstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.EncryptedLastName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email);
            builder.Property(e => e.PasswordHash).IsRequired();
            builder.Property(e => e.PasswordSalt).IsRequired();

            //İlişkiler
            // Employee - Department (Many-to-One)
            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .IsRequired();

            //SoftDelete için
            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

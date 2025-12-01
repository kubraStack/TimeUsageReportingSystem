using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SeedData
{
    public static class TimeLogSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeLog>().HasData(
                ////Admin (ID=1) için örnek zaman kayıtları
                //new TimeLog
                //{
                //    Id = 1,
                //    EmployeeId = 1,
                //    LogType= LogType.Entry,
                //    LogTime = new DateTime(2025, 10, 18, 9, 0, 0, DateTimeKind.Utc), // Sabit tarih
                //    CreatedDate = new DateTime(2004, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //},
                //new TimeLog
                //{
                //    Id = 2,
                //    EmployeeId = 1,
                //    LogType= LogType.Exit,
                //    LogTime = new DateTime(2025, 10, 18, 18, 0, 0, DateTimeKind.Utc), // Sabit tarih
                //    CreatedDate = new DateTime(2004, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //}

            );
        }
    }
}

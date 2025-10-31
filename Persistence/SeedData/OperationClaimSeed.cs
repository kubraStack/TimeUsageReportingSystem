using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SeedData
{
    public static class OperationClaimSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationClaim>().HasData(
                //Yönetim ve Genel İzinleri
                new OperationClaim { Id = 1, Name = "Admin" }, //Tüm yetkiler
                new OperationClaim { Id = 2, Name = "Manager" } ,// Yönetici yetkileri
                new OperationClaim { Id=3, Name="Employee"},

                //Çalışan Yönetimi İzinleri
                new OperationClaim { Id = 4, Name = "employee.add" }, // Standart kullanıcı yetkileri
                new OperationClaim { Id = 5, Name = "employee.update" },
                new OperationClaim { Id = 6, Name = "employee.delete" },
                new OperationClaim { Id =7, Name = "employee.list" },

                //Mesai (TimeLog) Yönetimi İzinleri
                new OperationClaim { Id = 8, Name = "timelog.add" }, //Giriş-Çıkış yapma
                new OperationClaim { Id = 9, Name = "timelog.listself" }, //Kendi loglarını görme
                new OperationClaim { Id = 10, Name = "timelog.listall" } //Tüm logları görme(Yönetici için)
            );
        }
    }
}

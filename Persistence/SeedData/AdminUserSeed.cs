using Core.Entities;
using Core.Utilities.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SeedData
{
    public static class AdminUserSeed
    {

        private const string ADMIN_PASSWORD_HASH_BASE64 = "SGVsbG8gV29ybGQh"; // "Hello World!" örnek
        private const string ADMIN_PASSWORD_SALT_BASE64 = "U2FsdCBFeGFtcGxl";  // "Salt Example" örnek

        private const string ADMIN_FNAME_ENCRYPTED = "UGFzc3dvcmRBZG1pbiBmaXJzdCBuYW1lIQ==";
        private const string ADMIN_LNAME_ENCRYPTED = "Q2xlYW5BcmNoaXRlY3R1cmUgU2VlZCBmaXg=";

        

        public static void Seed(ModelBuilder modelBuilder)
        {
            byte[] passwordHash = Convert.FromBase64String(ADMIN_PASSWORD_HASH_BASE64);
            byte[] passwordSalt = Convert.FromBase64String(ADMIN_PASSWORD_SALT_BASE64);

            //varsayılan departmanı ekle
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Yönetim Kurulu",
                    Description = "Sistem yöneticilerinin departmanı",
                    CreatedDate = new DateTime(2004,1,1,0,0,0, DateTimeKind.Utc),

                }
            );

            //yönetici(Admin) hesabını ekle
            var adminUser = new Employee
            {
                Id = 1,
                DepartmentId = 1,
                EncryptedFirstName = ADMIN_FNAME_ENCRYPTED,
                EncryptedLastName = ADMIN_LNAME_ENCRYPTED,
                Email = "admin@timesystem.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Core.Entities.UserType.Admin,
                CreatedDate = new DateTime(2004,1,1,0,0,0, DateTimeKind.Utc)
            };
            modelBuilder.Entity<Employee>().HasData(adminUser);

            //Admin kullanıcısına admin iznini atama
            modelBuilder.Entity<UserOperationClaim>().HasData(
                 new UserOperationClaim
                 {
                     Id = 1,
                     EmployeeId = adminUser.Id,
                     OperationClaimId = 1, // Admin OperationClaim Id'si
                     CreatedDate = new DateTime(2004, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                 }
            );

        }
    }
}

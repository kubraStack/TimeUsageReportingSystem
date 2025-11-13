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

        private static readonly string ADMIN_PASSWORD_HASH_BASE64_STATIC = "TKEhsfC7hXIsT1Z6ZKBHrGRtTwsRhDYDuAoaDnhJ/Nbe8ug+CXkMlJogCox7JxQpx2emdR01cUW26vzgQVjeUw==";
        private static readonly string ADMIN_PASSWORD_SALT_BASE64_STATIC = "hlbzNjpDs58CRpc2eCjgcuRl48/7BngZQEMwoKS3QRWLrxFBUQMJC8JEsFw9g/quMLyAkT85hI2y7W13sk/EiEBrWT80i5BuiRQZHa0RGZGR8LFxtz+qkdisrFFRaM7QWEuI3uEMPZ0FOoBNnsj9Qcs+yY17AznqVRsC3LXnFAA=";

        private const string ADMIN_FNAME_ENCRYPTED = "UGFzc3dvcmRBZG1pbiBmaXJzdCBuYW1lIQ==";
        private const string ADMIN_LNAME_ENCRYPTED = "Q2xlYW5BcmNoaXRlY3R1cmUgU2VlZCBmaXg=";


        public static void Seed(ModelBuilder modelBuilder)
        {
            byte[] passwordHash = Convert.FromBase64String(ADMIN_PASSWORD_HASH_BASE64_STATIC);
            byte[] passwordSalt = Convert.FromBase64String(ADMIN_PASSWORD_SALT_BASE64_STATIC);


            //varsayılan departmanı ekle
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Yönetim Kurulu",
                    Description = "Sistem yöneticilerinin departmanı",
                    CreatedDate = new DateTime(2004, 1, 1, 0, 0, 0),

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
                CreatedDate = new DateTime(2004, 1, 1, 0, 0, 0)
            };
            modelBuilder.Entity<Employee>().HasData(adminUser);

            //Admin kullanıcısına admin iznini atama
            modelBuilder.Entity<UserOperationClaim>().HasData(
                 new UserOperationClaim
                 {
                     Id = 1,
                     EmployeeId = adminUser.Id,
                     OperationClaimId = 3, // Admin OperationClaim Id'si
                     CreatedDate = new DateTime(2004, 1, 1, 0, 0, 0)
                 }
            );

        }
    }
}

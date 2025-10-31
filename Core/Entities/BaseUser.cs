using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class BaseUser : Entity
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Column("FirstName")]
        public string EncryptedFirstName { get; set; }
        [Column("LastName")]
        public string EncryptedLastName { get; set; }

        public UserType Role { get; set; }
    }

    public enum UserType
    {
        Employee = 1,
        Manager = 2,
        Admin = 3,

    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extentions
{
    public static class UserTypeExtentions
    {
        public static string ToDecriptionString(UserType val)
        {
            switch (val)
            {
                case UserType.Admin:
                    return "System Admin";
                case UserType.Employee:
                    return "Standard Employee";
                case UserType.Manager:
                    return "Manager";
                default:
                    return "Unknown";
            }
        }
    }
}

using Store_Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Store_Management.Utils
{
    public static class RoleUtil
    {
        public static string ToRoleName(this Employee.Role role) { 
            switch(role)
            {
                case Employee.Role.ADMIN:
                    return "Admin";

                case Employee.Role.STAFF:
                    return "Staff";

                default:
                    return "Staff";
            }
        }
        public static string ToRoleName(int roleId) { 
            switch(roleId)
            {
                case (int)Employee.Role.ADMIN:
                    return "Admin";

                case (int)Employee.Role.STAFF:
                    return "Staff";

                default:
                    return "Staff";
            }
        }

        public static Array ToArray()
        {
           return  Enum.GetValues(typeof(Employee.Role));
        }
    }
}

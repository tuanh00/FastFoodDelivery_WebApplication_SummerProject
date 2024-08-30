using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.Common
{
    public class UserRole
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Shipper = "Shipper";
        public static readonly List<string> Roles = new List<string> { Admin, User, Shipper };
    }
}

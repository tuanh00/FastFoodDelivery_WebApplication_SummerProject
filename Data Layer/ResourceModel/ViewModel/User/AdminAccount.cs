using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.User
{
    public class AdminAccount
    {
        public string username { get; set; }
        public string password { get; set; }
        public string Status { get; set; } = "Active";
    }
}

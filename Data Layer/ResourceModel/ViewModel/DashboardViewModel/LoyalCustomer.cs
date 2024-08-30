using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.DashboardViewModel
{
    public class LoyalCustomer
    {
        public string CustomerName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalCost { get; set; }
    }
}

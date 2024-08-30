using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.ShipperViewModels
{
    public class ShipperReport
    {
        public string ShipperName { get; set; }
        public int TotalReceivedOrders { get; set; }
        public int TotalShippedOrders { get; set; }        
    }
}

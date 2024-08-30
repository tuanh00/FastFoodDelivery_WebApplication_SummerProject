using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.OrderVMs
{
    public class OrderUpdateForShipperVM
    {
        public Guid? ShipperId { get; set; }
    }
}

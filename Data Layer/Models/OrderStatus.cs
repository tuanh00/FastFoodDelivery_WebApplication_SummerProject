using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class OrderStatus
    {
        public Guid OrderStatusId { get; set; }
        public string? ShipperId { get; set; }
        public Guid? OrderId { get; set; }
        public string? OrderStatusName { get; set; }

        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}

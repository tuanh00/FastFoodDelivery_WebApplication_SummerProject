using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string? MemberId { get; set; }
        public Guid? ShipperId { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string? Address { get; set; }
        public Decimal? TotalPrice { get; set; }
        public string? StatusOrder { get; set; }
        public string? DeliveryStatus { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<TransactionBill> TransactionBills { get; set; } = new List<TransactionBill>();
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();
        public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
    }
}

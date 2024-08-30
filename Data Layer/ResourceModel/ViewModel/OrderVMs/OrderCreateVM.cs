using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.OrderVMs
{
    public class OrderCreateVM
    {
        public string? MemberId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        //public string? Address { get; set; }
        public Decimal? TotalPrice { get; set; }
        public List<OrderDetaiCreateVM> OrderDetails { get; set; }
    }
}

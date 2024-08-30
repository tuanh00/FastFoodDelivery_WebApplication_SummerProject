using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel
{
    public class OrderDetailVM
    {
        public Guid? FoodId { get; set; }
        public string? FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
    }
}

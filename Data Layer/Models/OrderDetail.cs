using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? FoodId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }

        public virtual MenuFoodItem MenuFoodItem { get; set; }
        public virtual Order Order { get; set; }
    }
}

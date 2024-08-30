using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class MenuFoodItem
    {
        public Guid FoodId { get; set; }
        public Guid? CategoryId { get; set; }
        public string FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public string? Image { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? FoodStatus { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}

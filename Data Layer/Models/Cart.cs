using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public string? UserID { get; set; }
        public Guid foodId { get; set; }
        public int Quantity { get; set; }
        
        public virtual User User { get; set; }
        public virtual MenuFoodItem Food { get; set; }
    }
}

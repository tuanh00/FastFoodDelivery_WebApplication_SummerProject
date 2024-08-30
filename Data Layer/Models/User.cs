using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    }
}

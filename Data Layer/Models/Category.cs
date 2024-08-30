using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoriesName { get; set; }
        public string? CategoriesStatus { get; set; }

        public virtual ICollection<MenuFoodItem> MenuFoodItems { get; set; } = new List<MenuFoodItem>();
    }
}

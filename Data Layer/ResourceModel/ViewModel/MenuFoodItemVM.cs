using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel
{
    public class MenuFoodItemVM
    {
        public Guid FoodId { get; set; }
        public Guid? CategoryId { get; set; }
        public string FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public string? Image { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? FoodStatus { get; set; }
    }
}

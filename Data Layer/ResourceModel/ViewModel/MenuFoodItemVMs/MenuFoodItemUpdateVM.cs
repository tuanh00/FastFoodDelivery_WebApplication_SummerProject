

using System.ComponentModel.DataAnnotations;

namespace Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs
{
    public class MenuFoodItemUpdateVM
    {
        [Required]
        public Guid? CategoryId { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public string? FoodDescription { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public decimal? UnitPrice { get; set; }
    }
}

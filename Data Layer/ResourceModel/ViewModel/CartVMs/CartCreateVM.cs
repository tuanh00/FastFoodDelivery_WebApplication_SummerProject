using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.CartVMs
{
    public class CartCreateVM
    {
        public string? UserID { get; set; }
        public Guid foodId { get; set; }
        public int Quantity { get; set; }
    }
}

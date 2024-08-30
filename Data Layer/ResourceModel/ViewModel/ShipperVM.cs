using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel
{
    public class ShipperVM
    {
        public string? userId { get; set; }
        public List<Guid>? orderStatusId { get; set; }
    }
}

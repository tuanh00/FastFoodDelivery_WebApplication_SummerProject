using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.FeedBackVMs
{
    public class FeedBackCreateVM
    {
        public string? UserId { get; set; }
        public Guid OrderId { get; set; }
        public string CommentMsg { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class FeedBack
    {
        public Guid FeedBackId { get; set; }
        public string? UserId { get; set; }
        public Guid OrderId { get; set; }
        public string CommentMsg { get; set; }

        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}

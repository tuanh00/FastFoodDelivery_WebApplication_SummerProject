using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.Common
{
    public class APIResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
    }
}

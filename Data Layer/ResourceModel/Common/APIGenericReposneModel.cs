using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.Common
{
    public class APIGenericReposneModel<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.VNPay
{
    public class VNPaySettings
    {
        public string? Url { get; set; }
        public string? ReturnUrl { get; set; }
        public string? TmnCode { get; set; }
        public string? HashSecret { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Utils
{
    public class ZaloPaySettings
    {
        public static string ConfigName => "ZaloPay";
        public int AppId { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Endpoint { get; set; }
    }
}

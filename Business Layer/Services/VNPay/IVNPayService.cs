using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.VNPay
{
    public interface IVNPayService
    {
        Task<string> CreatePaymentRequestAsync(Guid orderId);
        Task<APIResponseModel> ConfirmPaymentAsync(IQueryCollection queryString);
    }
}

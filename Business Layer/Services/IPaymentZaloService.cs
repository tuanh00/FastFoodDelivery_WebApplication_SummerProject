using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IPaymentZaloService
    {
        public Task<string> CreatePaymentRequestAsync(OrderPaymentVM order);
    }
}

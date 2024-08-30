using Business_Layer.Repositories;
using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business_Layer.Services.VNPay
{
    public class VNPayService : IVNPayService
    {
        private readonly VNPaySettings _vnPaySettings;
        private readonly IOrderRepository _orderRepository;

        public VNPayService(IOptions<VNPaySettings> vnPaySettings, IOrderRepository orderRepository)
        {
            _vnPaySettings = vnPaySettings.Value;
            _orderRepository = orderRepository;
        }

        public async Task<string> CreatePaymentRequestAsync(Guid orderId)
        {
            // Check if order exists
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order doesn't exist.");
            }
            int amount = (int)(order.TotalPrice ?? 0);
            string orderInfo = DateTime.Now.Ticks.ToString();
            string hostName = System.Net.Dns.GetHostName();
            string clientIPAddress = System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();
            VNPayHelper pay = new VNPayHelper();
            string formattedAmount = amount.ToString() + "00"; // Format amount from DB to match that of VNPay
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", _vnPaySettings.TmnCode);
            pay.AddRequestData("vnp_Amount", formattedAmount);
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", order.OrderId.ToString()); // Use orderId as order info
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", _vnPaySettings.ReturnUrl);
            pay.AddRequestData("vnp_TxnRef", orderInfo);

            string paymentUrl = pay.CreateRequestUrl(_vnPaySettings.Url, _vnPaySettings.HashSecret);
            return paymentUrl;
        }
        public async Task<APIResponseModel> ConfirmPaymentAsync(IQueryCollection queryString)
        {
            var response = new APIResponseModel();

            var queryParameters = new Dictionary<string, string>();
            foreach (var key in queryString.Keys)
            {
                queryParameters[key] = queryString[key];
            }

            long orderId = Convert.ToInt64(queryParameters["vnp_TxnRef"]);
            string orderInfor = queryParameters["vnp_OrderInfo"];
            long vnpayTranId = Convert.ToInt64(queryParameters["vnp_TransactionNo"]);
            string vnp_ResponseCode = queryParameters["vnp_ResponseCode"];
            string vnp_SecureHash = queryParameters["vnp_SecureHash"];
            var rawData = new StringBuilder();
            foreach (var key in queryParameters.Keys.OrderBy(k => k))
            {
                if (key != "vnp_SecureHash")
                {
                    rawData.Append($"{key}={queryParameters[key]}&");
                }
            }
            // Remove the trailing '&'
            if (rawData.Length > 0)
            {
                rawData.Length -= 1;
            }

            bool checkSignature = ValidateSignature(rawData.ToString(), vnp_SecureHash, _vnPaySettings.HashSecret);

            if (checkSignature && _vnPaySettings.TmnCode == queryParameters["vnp_TmnCode"])
            {
                Guid id = new Guid(orderInfor);
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    throw new ArgumentException("Order doesn't exist.");
                }

                if (vnp_ResponseCode == "00")
                {
                    // Payment successful
                    order.StatusOrder = "Paid";
                    await _orderRepository.SaveAsync();
                    response.Data = order;
                    response.IsSuccess = true;
                    response.message = $"http://localhost:5173/paymentsuccess?vnp_Amount={queryParameters["vnp_Amount"]}&vnp_BankCode={queryParameters["vnp_BankCode"]}&vnp_BankTranNo={queryParameters["vnp_BankTranNo"]}&vnp_CardType={queryParameters["vnp_CardType"]}&vnp_OrderInfo={queryParameters["vnp_OrderInfo"]}&vnp_PayDate={queryParameters["vnp_PayDate"]}&vnp_ResponseCode={queryParameters["vnp_ResponseCode"]}&vnp_TmnCode={queryParameters["vnp_TmnCode"]}&vnp_TransactionNo={queryParameters["vnp_TransactionNo"]}&vnp_TransactionStatus={queryParameters["vnp_TransactionStatus"]}&vnp_TxnRef={queryParameters["vnp_TxnRef"]}&vnp_SecureHash={queryParameters["vnp_SecureHash"]}";
                    return response;
                }
                else
                {
                    // Payment failed
                    order.StatusOrder = "Pending";
                    await _orderRepository.SaveAsync();
                    response.Data = order;
                    response.IsSuccess = false;
                    response.message = $"http://localhost:5173/PaymentFail";
                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.message = $"Invalid response!";
                return response;
            }
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = VNPayHelper.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}

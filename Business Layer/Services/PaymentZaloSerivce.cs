using Business_Layer.Utils;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Business_Layer.Services
{
    public class PaymentZaloSerivce : IPaymentZaloService
    {
        private readonly ZaloPaySettings _settings;

        public Task<string> CreatePaymentRequestAsync(OrderPaymentVM order)
        {
            throw new NotImplementedException();
        }

        //public PaymentZaloSerivce(IOptions<ZaloPaySettings> settings)
        //{
        //    _settings = settings.Value;
        //}

        //public async Task<string> CreatePaymentRequestAsync(OrderPaymentVM order)
        //{
        //    var appTransId = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var amount = order.TotalPrice ?? 0;
        //    var orderInfo = GenerateOrderInfo(order);

        //    var requestData = new
        //    {
        //        app_id = _settings.AppId,
        //        app_user = order.MemberId ?? "anonymous",
        //        app_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        //        amount = (long)(amount * 100),
        //        app_trans_id = appTransId,
        //        embed_data = "{}",
        //        item = JsonConvert.SerializeObject(order.OrderDetails.Select(od => new { od.FoodName, od.Quantity, od.UnitPrice })),
        //        description = orderInfo,
        //        bank_code = "zalopayapp",
        //        mac = GenerateMac(appTransId, amount)
        //    };

        //    using var httpClient = new HttpClient();
        //    var response = await httpClient.PostAsync(_settings.Endpoint, new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json"));
        //    var responseData = await response.Content.ReadAsStringAsync();

        //    Console.WriteLine(responseData); 

        //    if (response.Content.Headers.ContentType.MediaType != "application/json")
        //    {
        //        throw new Exception("Unexpected response content type: " + response.Content.Headers.ContentType.MediaType);
        //    }

        //    try
        //    {
        //        dynamic result = JsonConvert.DeserializeObject(responseData);
        //        return result.order_url;
        //    }
        //    catch (JsonReaderException ex)
        //    {
        //        throw new Exception("Failed to parse response: " + responseData, ex);
        //    }
        //}

        //private string GenerateOrderInfo(OrderPaymentVM order)
        //{
        //    var orderDetails = order.OrderDetails.Select(od => $"{od.FoodName} (x{od.Quantity})").ToList();
        //    var orderInfo = $"Order {order.OrderId}: {string.Join(", ", orderDetails)}";
        //    return orderInfo;
        //}

        //private string GenerateMac(string appTransId, decimal amount)
        //{
        //    var rawData = $"{_settings.AppId}|{appTransId}|{(long)(amount * 100)}|{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}|{_settings.Key1}";
        //    using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_settings.Key2)))
        //    {
        //        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        //        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        //    }
        //}
    }

}

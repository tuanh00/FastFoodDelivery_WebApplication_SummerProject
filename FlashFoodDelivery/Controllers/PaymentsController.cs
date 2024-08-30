using Business_Layer.Services;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Data_Layer.ResourceModel.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Business_Layer.Repositories;
using Business_Layer.Services.VNPay;
using Microsoft.Extensions.Options;
using System.Web;
using Stripe.Climate;
using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Http.Extensions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentZaloService _paymentZaloService;
        private readonly VNPaySettings _vnPaySettings;
        private readonly IOrderService _orderService;

        private readonly IVNPayService _vnPayService;
        public PaymentsController(IPaymentZaloService paymentZaloService, IOptions<VNPaySettings> vnPaySettings, IOrderService orderService, IVNPayService vnPayService)
        {
            _paymentZaloService = paymentZaloService;
            _vnPaySettings = vnPaySettings.Value;
            _orderService = orderService;
            _vnPayService = vnPayService;
        }

        //[HttpPost]
        //[EnableCors("CorsPolicy")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> CreateZaloPayment([FromBody] OrderPaymentVM orderCreateVM)
        //{
        //    var paymentUrl = await _paymentZaloService.CreatePaymentRequestAsync(orderCreateVM);
        //    return Ok(new { url = paymentUrl });
        //}

        //[HttpPost]
        //[ProducesResponseType(typeof(BaseResultWithData<PaymentLinkDtos>), 200)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Create([FromBody] CreatePayment request)
        //{
        //    var response = new BaseResultWithData<PaymentLinkDtos>();
        //    response = await mediator.Send(request);
        //    return Ok(response);
        //}



        /**
         * param amount: số tiền
         * param id: orderId được tạo trong bảng order
         */
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("VNPay")]
        public async Task<IActionResult> VnPaymentRequest([FromBody] OrderPaymentVM model)
        {
            try
            {
                var paymentUrl = await _vnPayService.CreatePaymentRequestAsync(model.OrderId);
                return Ok(new { url = paymentUrl });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while processing the payment request.");
            }
        }

        [HttpGet("PaymentConfirm")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PaymentConfirm()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var result = await _vnPayService.ConfirmPaymentAsync(Request.Query);
                    if (result.IsSuccess && result.message.StartsWith("http"))
                    {
                        return Redirect(result.message);
                    }
                    else if (!result.IsSuccess && result.message.StartsWith("http"))
                    {
                        return Redirect(result.message);
                    }
                    else
                    {
                        return BadRequest(result.message);
                    }
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex);
                    return StatusCode(500, "An error occurred while processing the payment confirmation.");
                }
            }
            return StatusCode(500, "No query data");
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = VNPayHelper.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}


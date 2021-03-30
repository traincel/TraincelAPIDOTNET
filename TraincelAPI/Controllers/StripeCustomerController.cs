using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeCustomerController : ControllerBase
    {
        private IPaymentService _paymentService;
        public StripeCustomerController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("session")]
        public ResultsVM GetSession(OrdersVM ordersVM)
        {
            try
            {
                var session = _paymentService.GetSession(ordersVM);
                return new ResultsVM(session, Resources.Enums.StatusCodes.Success, null);
            } catch(Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in creating sessionId");
            }
        }
    }
}

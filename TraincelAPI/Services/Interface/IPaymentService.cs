using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface IPaymentService
    {
        public Session GetSession(OrdersVM orderVM);
    }
}

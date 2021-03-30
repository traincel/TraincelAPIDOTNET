using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.UM;

namespace TraincelAPI.Utilities
{
    public static class StripePayment
    {

        public static bool ProcessPayment(PaymentUM paymentUM)
        {
            StripeConfiguration.ApiKey = "sk_test_51Gzi07G3ZOAC0jt8niPlnlVoIWVWr7mNHGOFGXKwDPh4bFwDGSySN1miCyhS6yl2pqBfLbrPYUBZI61UAYtgtxkg00norJ7l27";

            var options = new ChargeCreateOptions
            {
                Amount = paymentUM.Amount,
                Currency = paymentUM.Currency.ToLower(),
                Source = paymentUM.Source,
                Description = paymentUM.Description,
            };
            var service = new ChargeService();
            service.Create(options);
            return true;
        }

    }
}

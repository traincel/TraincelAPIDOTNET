using AutoMapper;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services.Interface;

namespace TraincelAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IWebinarPurchasedOptionsDetailsRepo _webinarPurchasedOptionsDetailsRepo;

        public PaymentService(IMapper mapper, IWebinarPurchasedOptionsDetailsRepo webinarPurchasedOptionsDetailsRepo)
        {
            _mapper = mapper;
            _webinarPurchasedOptionsDetailsRepo = webinarPurchasedOptionsDetailsRepo;
        }

        public Session GetSession(OrdersVM ordersVM)
        {
            int? subTotal = 0;

            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            //testkey
          //  StripeConfiguration.ApiKey = "sk_test_51Gzi07G3ZOAC0jt8niPlnlVoIWVWr7mNHGOFGXKwDPh4bFwDGSySN1miCyhS6yl2pqBfLbrPYUBZI61UAYtgtxkg00norJ7l27";
            //activekey
            StripeConfiguration.ApiKey = "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";


            var options = new SessionCreateOptions();
            var lineItems = new List<SessionLineItemOptions>();
            options.PaymentMethodTypes = new List<string>
                {
                    "card",
                };
            options.Mode = "payment";
            options.SuccessUrl = "https://traincel.azurewebsites.net/order/success";
            options.CancelUrl = "https://traincel.azurewebsites.net/order/failure";
            options.LineItems = new List<SessionLineItemOptions>();
            ordersVM.OrderItems.ForEach((item) =>
            {
                var price = _webinarPurchasedOptionsDetailsRepo.GetWebinarPrice(item.WebinarId, item.PurchaseOptionId);
                string priceString = String.Concat(item.Price.ToString(), "00");
                price = Convert.ToInt32(priceString);

                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Webinar.WebinarName,
                        },

                        UnitAmountDecimal = price
                    },
                    Quantity = 1,
                };
                options.LineItems.Add(lineItem);
            });
            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
    }
}

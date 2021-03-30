using System;

namespace TraincelAPI.Models.VM
{
    public class WebinarPurchasedOptionsDetailsVM
    {

        public Guid Id { get; set; }
        public Guid WebinarId { get; set; }
        public int PurchaseOptionId { get; set; }
        public int? MaxCount { get; set; }
        public int? MaxDuration { get; set; }
        public int? Price { get; set; }

        public PurchaseOptionsVM PurchaseOption { get; set; }
        public WebinarsVM Webinar { get; set; }
    }
}

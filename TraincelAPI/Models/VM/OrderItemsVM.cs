using System;

namespace TraincelAPI.Models.VM
{
    public class OrderItemsVM
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? WebinarId { get; set; }
        public int? PurchaseOptionId { get; set; }
        public int? Price { get; set; }
        public virtual PurchaseOptionsVM PurchaseOption { get; set; }
        public virtual WebinarsVM Webinar { get; set; }
    }
}

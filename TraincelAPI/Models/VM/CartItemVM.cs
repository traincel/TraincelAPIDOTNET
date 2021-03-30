using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class CartItemVM
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid WebinarId { get; set; }
        public int Quantity { get; set; }
        public int PurchaseOptionId { get; set; }
        public virtual PurchaseOptionsVM PurchaseOption { get; set; }
        public virtual WebinarsVM Webinar { get; set; }
    }
}

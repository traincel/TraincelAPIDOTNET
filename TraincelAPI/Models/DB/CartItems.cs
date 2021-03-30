using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class CartItems
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public Guid CartId { get; set; }
        public Guid WebinarId { get; set; }
        public int Quantity { get; set; }
        public int PurchaseOptionId { get; set; }
        public int? WebinarLocalId { get; set; }

        public virtual UserCartMapping Cart { get; set; }
        public virtual PurchaseOptions PurchaseOption { get; set; }
        public virtual Webinars Webinar { get; set; }
    }
}

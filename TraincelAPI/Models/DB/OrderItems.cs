using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class OrderItems
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? WebinarId { get; set; }
        public int? PurchaseOptionId { get; set; }
        public int? Price { get; set; }
        public int? OrderLocalId { get; set; }
        public int? WebinarLocalId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual PurchaseOptions PurchaseOption { get; set; }
        public virtual Webinars Webinar { get; set; }
    }
}

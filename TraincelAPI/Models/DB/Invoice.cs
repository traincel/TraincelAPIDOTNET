using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class Invoice
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceUrl { get; set; }
        public int? OrderLocalId { get; set; }

        public virtual Orders Order { get; set; }
    }
}

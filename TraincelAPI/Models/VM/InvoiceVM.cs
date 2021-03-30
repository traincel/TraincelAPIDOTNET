using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class InvoiceVM
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceUrl { get; set; }
    }
}

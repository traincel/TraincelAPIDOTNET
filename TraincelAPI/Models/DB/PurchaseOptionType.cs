using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class PurchaseOptionType
    {
        public PurchaseOptionType()
        {
            PurchaseOptions = new HashSet<PurchaseOptions>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<PurchaseOptions> PurchaseOptions { get; set; }
    }
}

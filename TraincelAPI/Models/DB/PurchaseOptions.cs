using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class PurchaseOptions
    {
        public PurchaseOptions()
        {
            CartItems = new HashSet<CartItems>();
            OrderItems = new HashSet<OrderItems>();
            WebinarPurchasedOptionsDetails = new HashSet<WebinarPurchasedOptionsDetails>();
        }

        public int Id { get; set; }
        public string PurchasedOptionType { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? TypeId { get; set; }

        public virtual PurchaseOptionType Type { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<WebinarPurchasedOptionsDetails> WebinarPurchasedOptionsDetails { get; set; }
    }
}

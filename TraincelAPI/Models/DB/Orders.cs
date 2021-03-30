using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class Orders
    {
        public Orders()
        {
            Invoice = new HashSet<Invoice>();
            OrderItems = new HashSet<OrderItems>();
        }

        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public Guid? UserId { get; set; }
        public int? UserLocalId { get; set; }

        public virtual UserTable User { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}

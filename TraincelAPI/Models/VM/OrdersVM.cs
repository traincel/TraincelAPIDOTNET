using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.VM
{
    public class OrdersVM
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public UserDetailsVM User { get; set; }
        public List<OrderItemsVM> OrderItems { get; set; }
    }
}

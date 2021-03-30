using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class UserCartMapping
    {
        public UserCartMapping()
        {
            CartItems = new HashSet<CartItems>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int? UserLocalId { get; set; }

        public virtual UserTable User { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; }
    }
}

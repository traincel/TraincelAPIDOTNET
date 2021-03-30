using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class UserCartVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
      
        public virtual ICollection<CartItemVM> CartItems { get; set; }
    }
}

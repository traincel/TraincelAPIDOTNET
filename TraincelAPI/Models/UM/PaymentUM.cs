using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.UM
{
    public class PaymentUM
    {
        public int Amount { get; set; }
        public String Currency { get; set; }
        public String Source { get; set; }
        public String Description { get; set; }
    }
}

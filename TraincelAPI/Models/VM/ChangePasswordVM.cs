using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class ChangePasswordVM
    {
        public String UserId { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}

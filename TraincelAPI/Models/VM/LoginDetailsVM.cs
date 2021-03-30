using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class LoginDetailsVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string EmailId { get; set; }
        public bool IsLogIn { get; set; }
        public int RoleId{ get; set; }

        public UserDetailsVM User { get; set; }
    }
}

using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class CommonEmailVM
    {
        public String EmailAddress { get; set; }
        public String Name { get; set; }
        public String ContactNumber { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public String Industry { get; set; }
        public String Website { get; set; }
        public String Bio { get; set; }
        public String Company { get; set; }
        public String NotificationType { get; set; }
        public String ChangePasswordCode { get; set; }

        public String ApiKey { get; set; }
    }
}

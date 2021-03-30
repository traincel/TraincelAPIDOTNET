using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class RegisterVM
    {
        public Guid? Id { get; set; }
        public int? LocalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int CountryId { get; set; }
        public string Password { get; set; }
        public Guid? CompanyId { get; set; }
        public int? CompanyLocalId { get; set; }
        public string Designation { get; set; }
        public string CompanyName { get; set; }
    }
}

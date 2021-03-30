using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.VM
{
    public class UserDetailsVM
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int CountryId { get; set; }
        public Guid? CompanyId { get; set; }
        public string Designation { get; set; }

        public CompanyVM Company { get; set; }

        public CountriesVM Country { get; set; }
    }
}
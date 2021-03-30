using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class FacultiesVM
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int CountryId { get; set; }
        public string MobileNo { get; set; }
        public string Designation { get; set; }
        public string Bio { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? BecameFacultyOn { get; set; }
        public CountriesVM Country { get; set; }
        public List<WebinarsVM> Webinars { get; set; }
    }
}

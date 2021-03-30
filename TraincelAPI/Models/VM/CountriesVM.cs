using System.Collections.Generic;

namespace TraincelAPI.Models.VM
{
    public class CountriesVM
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public ICollection<FacultiesVM > Faculties { get; set; }
        public ICollection<UserDetailsVM> UserTable { get; set; }
    }
}
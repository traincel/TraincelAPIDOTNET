using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class Faculties
    {
        public Faculties()
        {
            Webinars = new HashSet<Webinars>();
        }

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
        public bool IsCurrent { get; set; }
        public DateTime? BecameFacultyOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Webinars> Webinars { get; set; }
    }
}

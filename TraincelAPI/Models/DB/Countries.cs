using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class Countries
    {
        public Countries()
        {
            Faculties = new HashSet<Faculties>();
            UserTable = new HashSet<UserTable>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<Faculties> Faculties { get; set; }
        public virtual ICollection<UserTable> UserTable { get; set; }
    }
}

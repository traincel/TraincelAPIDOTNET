using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class Company
    {
        public Company()
        {
            UserTable = new HashSet<UserTable>();
        }

        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<UserTable> UserTable { get; set; }
    }
}

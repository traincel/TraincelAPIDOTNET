using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class UserTable
    {
        public UserTable()
        {
            LoginTable = new HashSet<LoginTable>();
            Orders = new HashSet<Orders>();
            UserCartMapping = new HashSet<UserCartMapping>();
        }

        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int CountryId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid? CompanyId { get; set; }
        public string Designation { get; set; }
        public int? CompanyLocalId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Countries Country { get; set; }
        public virtual ICollection<LoginTable> LoginTable { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<UserCartMapping> UserCartMapping { get; set; }
    }
}

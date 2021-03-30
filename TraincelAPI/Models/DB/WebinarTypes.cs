using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class WebinarTypes
    {
        public WebinarTypes()
        {
            Webinars = new HashSet<Webinars>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<Webinars> Webinars { get; set; }
    }
}

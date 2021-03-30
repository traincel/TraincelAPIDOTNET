using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class LoginTable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsLogIn { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? UserLocalId { get; set; }
        public string PasswordChangeCode { get; set; }

        public virtual UserTable User { get; set; }
    }
}

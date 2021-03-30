using System;
using System.Collections.Generic;

namespace TraincelAPI.Models.DB
{
    public partial class Webinars
    {
        public Webinars()
        {
            CartItems = new HashSet<CartItems>();
            OrderItems = new HashSet<OrderItems>();
            WebinarPurchasedOptionsDetails = new HashSet<WebinarPurchasedOptionsDetails>();
        }

        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public string WebinarName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDeleted { get; set; }
        public int? WebinarTypeId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string ThumbImageUrl { get; set; }
        public string Duration { get; set; }
        public string Overview { get; set; }
        public string LearningObjectives { get; set; }
        public string ReasonToAttend { get; set; }
        public string AreasCovered { get; set; }
        public string WhoWillBenefit { get; set; }
        public Guid? FacultyId { get; set; }
        public string DateAndTime { get; set; }
        public int? FacultyLocalId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Faculties Faculty { get; set; }
        public virtual WebinarTypes WebinarType { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<WebinarPurchasedOptionsDetails> WebinarPurchasedOptionsDetails { get; set; }
    }
}

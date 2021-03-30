using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class WebinarsVM
    {
        public Guid Id { get; set; }
        public int LocalId { get; set; }
        public string WebinarName { get; set; }
        public bool IsFeatured { get; set; }
        public string ThumbImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string Duration { get; set; }
        public string DateAndTime { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Overview { get; set; }
        public string LearningObjectives { get; set; }
        public string ReasonToAttend { get; set; }
        public string AreasCovered { get; set; }
        public string WhoWillBenefit { get; set; }
        public Guid? FacultyId { get; set; }
        public int? WebinarTypeId { get; set; }
        public int? CategoryId { get; set; }
        public CategoriesVM Category { get; set; }
        public FacultiesVM Faculty { get; set; }
        public WebinarTypeVM WebinarType { get; set; }
        public virtual ICollection<CartItemVM> CartItems { get; set; }
        public virtual ICollection<OrderItemsVM> OrderItems { get; set; }
        public ICollection<WebinarPurchasedOptionsDetailsVM> WebinarPurchasedOptionsDetails { get; set; }
    }
}

using System.Collections.Generic;

namespace TraincelAPI.Models.VM
{
    public class PurchaseOptionsVM
    {
        public int Id { get; set; }
        public string PurchasedOptionType { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }

        public PurchaseOptionTypeVM Type { get; set; }
    }
}
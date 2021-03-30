using System;
using System.Collections.Generic;
using System.Linq;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface IPurchasedOptionsService
    {
        public List<PurchaseOptionsVM> GetPurchaseOptions();
        public bool AddPurchaseOptions(PurchaseOptionsVM purchaseOptions);
        public bool DeletePurchaseOptions(int id);

        #region PurchasedOptionType
        public bool AddPurchaseOptionType(PurchaseOptionTypeVM purchaseOptionType);
        public bool DeletePurchaseOptionType(int id);
        #endregion
    }
}

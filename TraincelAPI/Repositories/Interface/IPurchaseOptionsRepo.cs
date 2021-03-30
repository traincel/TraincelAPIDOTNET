using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IPurchaseOptionsRepo
    {
        public Task<List<PurchaseOptions>> GetPurchaseOptions();
        public Task<bool> AddPurchaseOption(PurchaseOptions purchaseOption);
        public Task<bool> DeletePurchaseOption(int id);
    }
}

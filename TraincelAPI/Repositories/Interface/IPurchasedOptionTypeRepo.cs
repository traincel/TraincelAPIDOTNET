using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IPurchasedOptionTypeRepo
    {
        public Task<bool> AddPurchasedOptionType(PurchaseOptionType purchasedOptionType);
        public Task<bool> DeletePurchasedOptionType(int id);
    }
}

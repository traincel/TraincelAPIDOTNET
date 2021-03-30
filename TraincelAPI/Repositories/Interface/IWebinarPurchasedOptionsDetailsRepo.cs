using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IWebinarPurchasedOptionsDetailsRepo
    {
        public Task<List<WebinarPurchasedOptionsDetails>> GetWebinarPurchasedOptionsDetails();
        public Task<WebinarPurchasedOptionsDetails> GetWebinarPurchasedOptionsDetails(Guid id);
        public Task<bool> AddWebinarPurchasedOptionsDetails(WebinarPurchasedOptionsDetails webinarPurchasedOptionsDetails);
        public  Task<bool> UpdateWebinarPurchasedOptionsDetails(WebinarPurchasedOptionsDetails webinarPurchasedOptionsDetails);
        public int? GetWebinarPrice(Guid? webinarId, int? purchasedOptionId);
    }
}

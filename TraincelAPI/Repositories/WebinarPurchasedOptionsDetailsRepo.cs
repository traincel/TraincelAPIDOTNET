using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class WebinarPurchasedOptionsDetailsRepo : IWebinarPurchasedOptionsDetailsRepo
    {
        private readonly TraincelContext _context;

        public WebinarPurchasedOptionsDetailsRepo(TraincelContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWebinarPurchasedOptionsDetails(WebinarPurchasedOptionsDetails webinarPurchasedOptionsDetails)
        {
            try
            {
                webinarPurchasedOptionsDetails.WebinarLocalId = _context.Webinars
                    .FirstOrDefaultAsync((webinar) => webinar.Id == webinarPurchasedOptionsDetails.WebinarId).Result.LocalId;
                _context.WebinarPurchasedOptionsDetails.Add(webinarPurchasedOptionsDetails);
                var response = await _context.SaveChangesAsync();
                return response >= 1;

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }
        public async Task<bool> UpdateWebinarPurchasedOptionsDetails(WebinarPurchasedOptionsDetails webinarPurchasedOptionsDetails)
        {
            try
            {
                var modelToBeUpdated = _context.WebinarPurchasedOptionsDetails.FirstOrDefaultAsync(webinarPrice => webinarPrice.Id == webinarPurchasedOptionsDetails.Id).Result;
                modelToBeUpdated.MaxCount = webinarPurchasedOptionsDetails.MaxCount;
                modelToBeUpdated.MaxDuration = webinarPurchasedOptionsDetails.MaxDuration;
                modelToBeUpdated.Price = webinarPurchasedOptionsDetails.Price;
                _context.Entry(modelToBeUpdated).State = EntityState.Modified;
                var response = await _context.SaveChangesAsync();
                return response >= 1;

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<WebinarPurchasedOptionsDetails>> GetWebinarPurchasedOptionsDetails()
        {
            try
            {
                return await _context.WebinarPurchasedOptionsDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WebinarPurchasedOptionsDetails> GetWebinarPurchasedOptionsDetails(Guid webinarId)
        {
           try
            {
               return await _context.WebinarPurchasedOptionsDetails.FirstOrDefaultAsync(webinar => webinar.WebinarId == webinarId);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int? GetWebinarPrice(Guid? webinarId, int? purchasedOptionId)
        {
            try
            {
                return  _context.WebinarPurchasedOptionsDetails.FirstOrDefault(purchasedOptions => purchasedOptions.WebinarId == webinarId && purchasedOptions.PurchaseOptionId == purchasedOptionId).Price;
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

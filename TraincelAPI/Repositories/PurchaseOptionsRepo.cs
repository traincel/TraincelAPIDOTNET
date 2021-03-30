using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class PurchaseOptionsRepo : IPurchaseOptionsRepo
    {
        private readonly TraincelContext _context;

        public PurchaseOptionsRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPurchaseOption(PurchaseOptions purchaseOption)
        {
            try
            {
                _context.PurchaseOptions.Add(purchaseOption);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<bool> DeletePurchaseOption(int id)
        {
            var purchaseOption = await _context.PurchaseOptions.FindAsync(id);
            if (purchaseOption == null)
            {
                throw new Exception("No such Purchase Options exist");
            }

            try
            {
                _context.PurchaseOptions.Remove(purchaseOption);
                var response = await _context.SaveChangesAsync();

                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<PurchaseOptions>> GetPurchaseOptions()
        {
            try
            {
                var purchaseOption = await _context.PurchaseOptions.ToListAsync();
                return (purchaseOption);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

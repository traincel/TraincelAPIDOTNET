using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class PurchasedOptionTypeRepo : IPurchasedOptionTypeRepo
    {

        private readonly TraincelContext _context;

        public PurchasedOptionTypeRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPurchasedOptionType(PurchaseOptionType purchasedOptionType)
        {
            try
            {
                _context.PurchaseOptionType.Add(purchasedOptionType);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<bool> DeletePurchasedOptionType(int id)
        {
            var purchasedOptionType = await _context.PurchaseOptionType.FindAsync(id);
            if (purchasedOptionType == null)
            {
                throw new Exception("No such Purchase Option Type exist");
            }

            try
            {
                _context.PurchaseOptionType.Remove(purchasedOptionType);
                var response = await _context.SaveChangesAsync();

                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }
    }
}

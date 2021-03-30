using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class CategoriesRepo : ICategoriesRepo
    {

        private readonly TraincelContext _context;

        public CategoriesRepo(TraincelContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(Categories categories)
        {           
            try
            {
                _context.Categories.Add(categories);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                throw new Exception("No such Category exist");
            }

            try
            {
                _context.Categories.Remove(categories);
                var response = await _context.SaveChangesAsync();

                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<Categories>> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return (categories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

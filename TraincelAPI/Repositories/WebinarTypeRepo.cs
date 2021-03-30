using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public class WebinarTypeRepo : IWebinarTypeRepo
    {
        private readonly TraincelContext _context;

        public WebinarTypeRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<bool> AddWebinarType(WebinarTypes webinarType)
        {
            try
            {
                _context.WebinarTypes.Add(webinarType);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeleteWebinarType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WebinarTypes>> GetWebinarTypes()
        {
            try
            {
                return await _context.WebinarTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

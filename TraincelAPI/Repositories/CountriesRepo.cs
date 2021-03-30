using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class CountriesRepo : ICountriesRepo
    {
        private readonly TraincelContext _context;

        public CountriesRepo(TraincelContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCountry(Countries country)
        {
            try
            {
                _context.Countries.Add(country);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<Countries>> GetCountries()
        {
            try
            {
                var countries = await _context.Countries.ToListAsync();
                return (countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

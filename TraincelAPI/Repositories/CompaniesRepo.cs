using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class CompaniesRepo : ICompaniesRepo
    {
        private readonly TraincelContext _context;

        public CompaniesRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddComapny(Company company)
        {
            try
            {
               _context.Company.Add(company);
                var response = await _context.SaveChangesAsync();
                return response >= 1 ? company.Id : throw new Exception("Error in adding new company");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Company.ToListAsync();
        }

        public async Task<Company> GetCompany(Guid? companyId)
        {
            return await _context.Company.FirstOrDefaultAsync(company => company.Id.Equals(companyId));
        }
    }
}

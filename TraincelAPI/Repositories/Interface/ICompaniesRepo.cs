using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface ICompaniesRepo
    {
        public Task<List<Company>> GetCompanies();
        public Task<Guid> AddComapny(Company company);
        public Task<Company> GetCompany(Guid? companyId);
    }
}
 
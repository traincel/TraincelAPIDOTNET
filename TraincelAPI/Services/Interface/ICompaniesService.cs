using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface ICompaniesService
    {
        public Guid AddCompany(String companyName);
        public List<CompanyVM> GetCompanies();
        public bool CompanyExist(Guid? companyId);
    }
}

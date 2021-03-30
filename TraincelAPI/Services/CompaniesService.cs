using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services.Interface;

namespace TraincelAPI.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepo _companiesRepo;
        private readonly IMapper _mapper;
        public CompaniesService(ICompaniesRepo companiesRepo, IMapper mapper)
        {
            _companiesRepo = companiesRepo;
            _mapper = mapper;
        }
        public Guid AddCompany(String companyName)
        {
            try
            {
                var company = new Company
                {
                    Id = Guid.NewGuid(),
                    CompanyName = companyName
                };
                var response = _companiesRepo.AddComapny(company).Result;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CompanyExist(Guid? companyId)
        {
            var company = _companiesRepo.GetCompany(companyId);
            return !(String.IsNullOrEmpty(company.Result.CompanyName));
        }

        public List<CompanyVM> GetCompanies()
        {
            try
            {
                var categories = _companiesRepo.GetCompanies().Result;
                return _mapper.Map<List<CompanyVM>>(categories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

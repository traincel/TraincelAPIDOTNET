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
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepo _countriesRepo;
        private readonly IMapper _mapper;
        public CountriesService(ICountriesRepo countriesRepo, IMapper mapper)
        {
            _countriesRepo = countriesRepo;
            _mapper = mapper;
        }

        public bool AddCountry(CountriesVM countryVM)
        {
            var country = _mapper.Map<Countries>(countryVM);
            var response = _countriesRepo.AddCountry(country);
            return (response.Result);
        }

        public List<CountriesVM> GetCountries()
        {
            var countries = _countriesRepo.GetCountries();
            return _mapper.Map<List<CountriesVM>>(countries.Result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface ICountriesRepo
    {
        public Task<List<Countries>> GetCountries();
        public Task<bool> AddCountry(Countries country);
    }
}

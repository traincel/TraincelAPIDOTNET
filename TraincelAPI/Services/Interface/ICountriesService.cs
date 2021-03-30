using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface ICountriesService
    {
        public List<CountriesVM> GetCountries();
        public bool AddCountry(CountriesVM countries);      

    }
}

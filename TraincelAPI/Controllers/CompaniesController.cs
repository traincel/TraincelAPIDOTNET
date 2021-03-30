using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;
using static TraincelAPI.Resources.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService _companiesService;
        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        // GET: api/<CompaniesController>
        [HttpGet]
        public ResultsVM GetCompanies()
        {
            try
            {
                var companies = _companiesService.GetCompanies();
                return new ResultsVM(companies, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in getting companies list");
            }
        }        
    }
}

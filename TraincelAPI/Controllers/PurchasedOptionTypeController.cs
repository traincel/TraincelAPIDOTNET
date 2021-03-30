using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedOptionTypeController : ControllerBase
    {
        private readonly IPurchasedOptionsService _purchasedOptionService;

        public PurchasedOptionTypeController(IPurchasedOptionsService purchasedOptionService)
        {
            _purchasedOptionService = purchasedOptionService;
        }

        // POST api/<PurchasedOptionTypeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        // DELETE api/<PurchasedOptionTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

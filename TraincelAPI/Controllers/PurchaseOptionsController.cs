using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOptionsController : ControllerBase
    {
        private readonly IPurchasedOptionsService _purchasedOptionsService;

        public PurchaseOptionsController(IPurchasedOptionsService purchasedOptionsService)
        {
            _purchasedOptionsService = purchasedOptionsService;
        }


        // GET: api/<PurchaseOptionsController>
        [HttpGet]
        public ActionResult<List<PurchaseOptionsVM>> GetPurchaseOptions()
        {
            try
            {
                var purchaseOptions = _purchasedOptionsService.GetPurchaseOptions();
                return Ok(purchaseOptions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/<PurchaseOptionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PurchaseOptionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PurchaseOptionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PurchaseOptionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

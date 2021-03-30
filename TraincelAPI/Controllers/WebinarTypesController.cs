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
    public class WebinarTypesController : ControllerBase
    {
        private IWebinarService _webinarService;
        public WebinarTypesController(IWebinarService webinarService)
        {
            _webinarService = webinarService;
        }

        // GET: api/<WebinarTypes>
        [HttpGet]
        public ActionResult<List<WebinarTypeVM>> Get()
        {
            try
            {
                return Ok(_webinarService.GetWebinarType());
            } catch (Exception)
            {
                throw new Exception("Unable to get webinarlist");
            }
        }

        // GET api/<WebinarTypes>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WebinarTypes>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WebinarTypes>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WebinarTypes>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

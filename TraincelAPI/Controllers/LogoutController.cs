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
    public class LogoutController : ControllerBase
    {

        private readonly IUserService _userService;
        public LogoutController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<LogoutController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LogoutController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LogoutController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LogoutController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogoutController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPatch()]
        public ActionResult<bool> PatchLoginTable(LoginDetailsVM patchData)
        {
            try
            {
                var response = _userService.UpdateLoginUserStatus(patchData.UserId, patchData.IsLogIn);
                return Ok(response);
            }
            catch (Exception)
            {
                throw new Exception("Unable to login");
            }
        }
    }
}

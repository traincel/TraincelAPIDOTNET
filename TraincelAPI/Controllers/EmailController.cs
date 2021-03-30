using System;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        // POST api/<EmailController>
        [HttpPost]
        public ResultsVM Post([FromBody] CommonEmailVM commonEmailVM)
        {
            try
            {
                var result = _emailService.SendEmail(commonEmailVM);
                return new ResultsVM(result, Resources.Enums.StatusCodes.Success, null);

            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, ex.Message);
            }
        }
    }
}

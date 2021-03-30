using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        // PUT api/<Login>/5
        [HttpPatch]
        public ActionResult<bool> UpdateLoginStatus([FromBody] ChangePasswordVM changePasswordVM)
        {
            try
            {
                var response = _userService.UpdateLoginPassword(changePasswordVM);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to login");
            }
        }



        [HttpPost("")]
        public ResultsVM GetLoginTable(LoginRequestVM loginCredetials)
        {
            try
            {
                var response = _userService.GetLoginDetails(loginCredetials);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);              
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, ex.Message);
            }
        }

        [HttpPost("admin")]
        public ResultsVM GetAdminLoginTable(AdminLoginRequestVM loginCredetials)
        {
            try
            {
                return new ResultsVM(_userService.CheckIfUserIsAdmin(loginCredetials), Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception)
            {
                throw new Exception("Error");
            }
        }
    }
}

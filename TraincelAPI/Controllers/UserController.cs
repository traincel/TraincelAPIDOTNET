using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<User>
        [HttpGet]
        public ActionResult<List<UserDetailsVM>> GetUsers()
        {
           try
            {
                var response = _userService.GetUsers();
                return Ok(response);
            }
            catch(Exception)
            {
                throw new Exception("Error In querying");
            }
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public ActionResult<UserDetailsVM> GetUser(Guid id)
        {
            try
            {
                var response = _userService.GetUser(id.ToString());
                return Ok(response);
            }
            catch (Exception)
            {
                throw new Exception("Error In querying");
            }
        }

        // POST api/<User>
        [HttpPost]
        public ResultsVM Post([FromBody] RegisterVM registerVM)
        {
            try
            {
                var response = _userService.AddUser(registerVM);
                return new ResultsVM(response,Resources.Enums.StatusCodes.Success, null);
            } catch(Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, ex.Message);
            }
        }

        // PUT api/<User>/5
        [HttpPut("{userId}")]
        public ResultsVM Put(Guid userId, [FromBody] RegisterVM userDetails)
        {
            try
            {
                var response = _userService.UpdateUserDetails(userDetails);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error In Updating User");
            }
        }

        [HttpPost("forgotPassword/sentCode")]
        public ResultsVM Post([FromBody] LoginRequestVM loginRequestVM)
        {
            try
            {
                var response = _userService.ForgotPasswordRequest(loginRequestVM.EmailId);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, "A code have been sent to your email Id");
            }
            catch(Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, ex.Message);
            }
        }

    }
}

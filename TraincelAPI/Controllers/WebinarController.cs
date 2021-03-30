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
    public class WebinarController : ControllerBase
    {
        private readonly IWebinarService _webinarService;

        public WebinarController(IWebinarService webinarService)
        {
            _webinarService = webinarService;
        }
        // GET: api/<WebinarController>
        [HttpGet("{userId}")]
        public ActionResult<List<WebinarsVM>> GetWebinars(String userId, int typeId = 0, int categoryId = 0)
        {
            try
            {
                var webinars = _webinarService.GetWebinars(typeId, categoryId, userId);
                return Ok(webinars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("search/{userId}")]
        public ResultsVM SearchWebinars(String userId, string searchTerm, int typeId = 0, int categoryId = 0)
        {
            try
            {
                var webinars = _webinarService.SearchWebinars(searchTerm, typeId, categoryId, userId);
                return new ResultsVM(webinars, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.Success, "Error in searching webinars");
            }
        }

        // GET api/<WebinarController>/5
        [HttpGet("{userId}/{webinarId}")]
        public ActionResult<WebinarsVM> GetWebinar(String userId, Guid webinarId)
        {
            var webinar = _webinarService.GetWebinar(userId, webinarId);
            return Ok(webinar);
        }

        [HttpGet("featured/{userId}")]
        public ActionResult<IEnumerable<WebinarsVM>> GetFeaturedWebinars(String userId)
        {
            try
            {
                return Ok(_webinarService.GetFeaturedWebinars(userId));
            }
            catch (Exception)
            {
                throw new Exception("Error");
            }
        }

        // POST api/<WebinarController>
        [HttpPost]
        public ResultsVM Post([FromBody] WebinarsVM webinar)
        {
            try
            {
                var response = _webinarService.AddWebinar(webinar);

                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);

            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in adding new webinar");
            }
        }

        // PUT api/<WebinarController>/5
        [HttpPost("updatePrice")]
        public ResultsVM UpdateWebinarPrice([FromBody] List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetailsVM)
        {
            try
            {
                var response = _webinarService.UpdateWebinarPrice(webinarPurchasedOptionsDetailsVM);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.Success, "Error in updating webinar price");
            }
        }

        [HttpPut("updateDetails")]
        public ResultsVM UpdateWebinarDetails([FromBody] WebinarsVM webinarsVM)
        {
            try
            {
                var response = _webinarService.UpdateWebinar(webinarsVM);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.Success, "Error in updating webinar price");
            }
        }

        // DELETE api/<WebinarController>/5
        [HttpDelete("{id}")]
        public ResultsVM Delete(string id)
        {
            try
            {
                var response = _webinarService.DeleteWebinar(id);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.Success, "Error in deleting webinar");
            }
        }

        [HttpGet("recentlyUpdated")]
        public ActionResult<List<WebinarsVM>> GetRecentlyUpdatedWebinars()
        {
            try
            {
                var webinars = _webinarService.GetRecentlyUpdatedWebinars();
                return Ok(webinars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("homePage/{userId}")]
        public ActionResult<List<WebinarsVM>> GetHomePageWebinars(String userId)
        {
            try
            {
                var webinars = _webinarService.GetHomePageWebinars(userId);
                return Ok(webinars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

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
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultiesService _facultiesService;

        public FacultiesController(IFacultiesService facultiesService)
        {
            _facultiesService = facultiesService;
        }

        // GET: api/<FacultiesController>
        [HttpGet]
        public ResultsVM GetFaculties()
        {
            try
            {
                var faculties = _facultiesService.GetFaculties();
                return new ResultsVM(faculties, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in querying faculties");
            }
        }

        [HttpGet("search")]
        public ResultsVM SearchFaculties(string searchTerm)
        {
            try
            {
                var faculties = _facultiesService.GetFaculties();
                return new ResultsVM(faculties, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in searching faculties");
            }
        }

        // GET api/<FacultiesController>/5
        [HttpGet("{facultyId}")]
        public ResultsVM GetFaculty(Guid facultyId)
        {
            try
            {
                var faculty = _facultiesService.GetFaculty(facultyId);
                return new ResultsVM(faculty, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in querying faculty with id: " + facultyId);
            }
        }

        [HttpGet("partialList")]
        public ResultsVM GetFacultiesList()
        {
            try
            {
                var faculties = _facultiesService.GetFacultiesForAdminConsole();
                return new ResultsVM(faculties, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in querying faculties");
            }
        }

        // POST api/<FacultiesController>
        [HttpPost]
        public ResultsVM Post([FromBody] FacultiesVM faculty)
        {
            try
            {
               var response = _facultiesService.AddFaculty(faculty);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            } catch(Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in  adding new Faculty member");
            }
        }

        // PUT api/<FacultiesController>/5
        [HttpPut("updateFacultyDetails")]
        public ResultsVM Put([FromBody] FacultiesVM faculty)
        {
            try
            {
                var response = _facultiesService.UpdateFaculty(faculty);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in  adding new Faculty member");
            }
        }

        // DELETE api/<FacultiesController>/5
        [HttpDelete("{id}")]
        public ResultsVM Delete(string id)
        {
            try
            {
                var response = _facultiesService.DeleteFaculty(id);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in  deleting the faculty member");
            }
        }
    }
}

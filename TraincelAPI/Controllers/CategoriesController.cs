using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Resources;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Repository
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api/<CategoriesRepository>
        [HttpGet]
        public ActionResult<List<CategoriesVM>> GetCategories()
        {
            try
            {
                var categories = _categoriesService.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }

        // POST api/<CategoriesRepository>
        [HttpPost]
        public ActionResult<bool> Post([FromBody] CategoriesVM categoriesVM)
        {
            var response = _categoriesService.AddCategory(categoriesVM);
            return Ok(response);
        }

        // DELETE api/<CategoriesRepository>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var response = _categoriesService.DeleteCategory(id);
            return Ok(response);
        }
    }
}

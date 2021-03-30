using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IS3Service _s3Service;
        public S3Controller(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }

        // GET: api/<BlobStorage>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BlobStorage>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlobStorage>
        [HttpPost("uploadFile")]
        public ResultsVM UploadFile(IFormFile asset, string readerName, string subFolderName)
        {
            try
            {
                var response = _s3Service.UploadFile(asset, readerName, subFolderName);
                return new ResultsVM(response, Resources.Enums.StatusCodes.Success, null);
            }
            catch
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error");
            }
        }


        // PUT api/<BlobStorage>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlobStorage>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

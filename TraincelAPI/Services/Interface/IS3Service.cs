using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Services.Interface
{
    public interface IS3Service
    {
        public Task<String> UploadFile(IFormFile file, string readerName, string subFolderName);
    }
}

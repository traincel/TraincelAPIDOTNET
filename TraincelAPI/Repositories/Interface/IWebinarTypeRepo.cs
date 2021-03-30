using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IWebinarTypeRepo
    {
        public Task<List<WebinarTypes>> GetWebinarTypes();
        public Task<bool> AddWebinarType(WebinarTypes webinarType);
        public Task<bool> DeleteWebinarType(int id);
    }
}

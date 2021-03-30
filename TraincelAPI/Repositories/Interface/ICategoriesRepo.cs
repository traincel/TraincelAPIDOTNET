using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface ICategoriesRepo
    {
        public Task<List<Categories>> GetCategories();
        public Task<bool> AddCategory(Categories categories);
        public Task<bool> DeleteCategory(int id);
    }
}

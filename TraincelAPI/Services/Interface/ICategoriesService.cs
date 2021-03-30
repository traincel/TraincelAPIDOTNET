using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface ICategoriesService
    {
        public List<CategoriesVM> GetCategories();
        public bool AddCategory(CategoriesVM categoriesVM);
        public bool DeleteCategory(int id);
    }
}

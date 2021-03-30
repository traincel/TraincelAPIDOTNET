using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services.Interface;

namespace TraincelAPI.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepo _categoriesRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepo categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public List<CategoriesVM> GetCategories()
        {
            try
            {
                var categories = _categoriesRepository.GetCategories();
                return _mapper.Map<List<CategoriesVM>>(categories.Result);
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool AddCategory(CategoriesVM categoriesVM)
        {
            try
            {
                var categories = _mapper.Map<Categories>(categoriesVM);
                var response = _categoriesRepository.AddCategory(categories);
                return (response.Result);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCategory(int id)
        {
            var response = _categoriesRepository.DeleteCategory(id);
            return response.Result;
        }

       
    }
}

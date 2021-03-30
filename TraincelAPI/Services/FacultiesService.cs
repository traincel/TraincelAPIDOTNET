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
    public class FacultiesService: IFacultiesService
    {
        private readonly IFacultiesRepo _facultiesRepo;
        private readonly IMapper _mapper;
        public FacultiesService(IFacultiesRepo facultiesRepo, IMapper mapper)
        {
            _facultiesRepo = facultiesRepo;
            _mapper = mapper;
        }

        public bool AddFaculty(FacultiesVM facultyVM)
        {
            var faculty = _mapper.Map<Faculties>(facultyVM);
            faculty.Id = Guid.NewGuid();
            var response = _facultiesRepo.AddFaculty(faculty);
            return (response.Result);
        }

        public bool DeleteFaculty(string id)
        {
            try
            {
                var response = _facultiesRepo.DeleteFaculty(new Guid(id));
                return response.Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<FacultiesVM> GetFaculties()
        {
            try
            {
                var faculties = _facultiesRepo.GetFaculties().Result;
                return _mapper.Map<List<FacultiesVM>>(faculties);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public FacultiesVM GetFaculty(Guid id)
        {
            try
            {
                var faculty = _facultiesRepo.GetFaculty(id).Result;
                return _mapper.Map<FacultiesVM>(faculty);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PartialFacultiesVM> GetFacultiesForAdminConsole()
        {
            try
            {
                var faculties = _facultiesRepo.GetFaculties();
                return _mapper.Map<List<PartialFacultiesVM>>(faculties.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public List<FacultiesVM> SearchFaculties(string searchTerm)
        {
            try
            {
                var faculties = GetFaculties();
                return faculties.Where(faculty => faculty.FirstName.ToLower().Contains(searchTerm.ToLower()) || faculty.LastName.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateFaculty(FacultiesVM facultyVM)
        {
            var faculty = _mapper.Map<Faculties>(facultyVM);
            var response = _facultiesRepo.UpdateFaculty(faculty);
            return (response.Result);
        }
    }
}

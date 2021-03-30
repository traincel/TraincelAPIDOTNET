using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IFacultiesRepo
    {
        public Task<List<Faculties>> GetFaculties();
        public Task<bool> AddFaculty(Faculties faculty);
        public Task<bool> DeleteFaculty(Guid id);
        public Task<Faculties> GetFaculty(Guid id);
        public Task<bool> UpdateFaculty(Faculties faculty);


    }
}

using System;
using System.Collections.Generic;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface IFacultiesService
    {
        public List<FacultiesVM> GetFaculties();
        public bool AddFaculty(FacultiesVM faculties);
        public bool DeleteFaculty(string id);
        public FacultiesVM GetFaculty(Guid id);
        public List<PartialFacultiesVM> GetFacultiesForAdminConsole();
        public List<FacultiesVM> SearchFaculties(string searchTerm);
        public bool UpdateFaculty(FacultiesVM facultyVM);
    }
}

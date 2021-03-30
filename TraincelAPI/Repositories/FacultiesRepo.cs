using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class FacultiesRepo : IFacultiesRepo
    {

        private readonly TraincelContext _context;

        public FacultiesRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<bool> AddFaculty(Faculties faculty)
        {
            try
            {
                faculty.IsCurrent = true;
                faculty.BecameFacultyOn = DateTime.Now;
                _context.Faculties.Add(faculty);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<bool> DeleteFaculty(Guid id)
        {
            var faculty = await _context.Faculties.FirstOrDefaultAsync(faculty => faculty.Id == id);
            if (faculty == null)
            {
                throw new Exception("No such Faculty exist");
            }

            try
            {
                _context.Faculties.Remove(faculty);
                var response = await _context.SaveChangesAsync();

                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<Faculties>> GetFaculties()
        {
            try
            {
                var faculties = await _context.Faculties.ToListAsync();
                return (faculties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Faculties> GetFaculty(Guid id)
        {
            try
            {
                var faculty = await _context.Faculties
               .Include(faculty => faculty.Webinars).Include(faculty => faculty.Webinars).FirstOrDefaultAsync(faculty => faculty.Id == id && faculty.IsCurrent);
                return faculty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateFaculty(Faculties faculty)
        {
            try
            {
                var modelToBeUpdated = _context.Faculties.FirstOrDefaultAsync(webinarDetail => webinarDetail.Id == faculty.Id).Result;
                modelToBeUpdated.FirstName = faculty.FirstName;
                modelToBeUpdated.LastName = faculty.LastName;
                modelToBeUpdated.EmailId = faculty.EmailId;
                modelToBeUpdated.MobileNo = faculty.MobileNo;
                modelToBeUpdated.Bio = faculty.Bio;
                modelToBeUpdated.CountryId = faculty.CountryId;
                modelToBeUpdated.Designation = faculty.Designation;

                _context.Entry(modelToBeUpdated).State = EntityState.Modified;
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
    }
}

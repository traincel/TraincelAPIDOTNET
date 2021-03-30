using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly TraincelContext _context;

        public UserRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUsers(UserTable user)
        {
            try
            {
                if (user.CompanyId.HasValue)
                {
                    user.CompanyLocalId = _context.Company.FirstOrDefaultAsync(company => company.Id == user.CompanyId).Result.LocalId;
                }
                _context.UserTable.Add(user);
                var response = await _context.SaveChangesAsync();
                if (response >= 1)
                {
                    var userCart = new UserCartMapping()
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        UserLocalId = user.LocalId
                    };
                    _context.UserCartMapping.Add(userCart);
                }
                var response1 = await _context.SaveChangesAsync();
                return response1 >= 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUsers(Guid id)
        {
            var userTable = await _context.UserTable.FirstOrDefaultAsync(user => user.Id == id);
            if (userTable == null)
            {
                return true;
            }

            _context.UserTable.Remove(userTable);
            var response = await _context.SaveChangesAsync();
            return response >= 1;
        }

        public async Task<UserTable> GetUser(Guid userId)
        {
            try
            {
                return await _context.UserTable.FirstOrDefaultAsync(user => user.Id == userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserTable> GetUserByEmail(String emailId)
        {
            try
            {
                return await _context.UserTable.FirstOrDefaultAsync(user => user.EmailId == emailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserTable>> GetUsers()
        {
            try
            {
                return await _context.UserTable
                    .Include(user => user.Company)
                    .Include(user => user.Country)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateUser(UserTable userTable)
        {
            var modelToBeUpdated = GetUser(userTable.Id).Result;
            modelToBeUpdated.LocalId = modelToBeUpdated.LocalId;
            modelToBeUpdated.FirstName = userTable.FirstName;
            modelToBeUpdated.LastName = userTable.LastName;
            modelToBeUpdated.EmailId = userTable.EmailId;
            modelToBeUpdated.MobileNo = userTable.MobileNo;
            modelToBeUpdated.CompanyId = userTable.CompanyId;
            modelToBeUpdated.Designation = userTable.EmailId;
            modelToBeUpdated.CountryId = userTable.CountryId;
            _context.Entry(modelToBeUpdated).State = EntityState.Modified;
            try
            {
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);

            }
        }

        public async Task<bool> UpdateChangePasswordCode(Guid userId, string code)
        {
            var modelToBeUpdated = _context.LoginTable.FirstOrDefaultAsync(userLoginDetails => userLoginDetails.UserId == userId).Result;
            if(modelToBeUpdated == null)
            {
                throw new Exception("No login details found for the user. Plaese contact customer care for more info, or try registering with another Mail Id");
            }
            modelToBeUpdated.PasswordChangeCode = code;
            var result = await _context.SaveChangesAsync();
            return result >= 1;
        }
    }
}

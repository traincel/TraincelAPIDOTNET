using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class LoginRepo : ILoginRepo
    {
        private readonly TraincelContext _context;

        public LoginRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<bool> AddLoginUser(LoginTable loginUser)
        {
            _context.LoginTable.Add(loginUser);
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

        public async Task<LoginTable> GetLoginDetails(String emailId)
        {
            try
            {
                return await _context.LoginTable.Include(login => login.User).FirstOrDefaultAsync(loginTable => loginTable.EmailId == emailId);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginTable> GetUserLoginDetails(String userId)
        {
            return await _context.LoginTable.FirstOrDefaultAsync(loginTable => loginTable.UserId == new Guid(userId));
        }

        public async Task<bool> UpdateLoginUserStatus(String userId, bool isLogin)
        {
            var modelToBeUpdated = GetUserLoginDetails(userId).Result;
            modelToBeUpdated.IsLogIn = isLogin;
            _context.Entry(modelToBeUpdated).State = EntityState.Modified;
            try
            {
              var response =  await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);

            }
        }
        public async Task<bool> UpdateLoginPassword(ChangePasswordVM changePasswordVM)
        {
            var modelToBeUpdated = GetUserLoginDetails(changePasswordVM.UserId).Result;
            if(modelToBeUpdated.PasswordChangeCode == changePasswordVM.Code)
            {
                modelToBeUpdated.Password = changePasswordVM.Password;
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
            } else
            {
                throw new Exception("Incorrect Code");
            }
           
            
        }

        private LoginTable CreatePatchModel(LoginTable partialModel, LoginTable modelToBeUpdate)
        {
            modelToBeUpdate.EmailId = partialModel.EmailId ?? modelToBeUpdate.EmailId;
            modelToBeUpdate.Password = partialModel.Password ?? modelToBeUpdate.Password;
            modelToBeUpdate.IsLogIn = partialModel.IsLogIn ? modelToBeUpdate.IsLogIn : partialModel.IsLogIn;
            return modelToBeUpdate;
        }
    }
}

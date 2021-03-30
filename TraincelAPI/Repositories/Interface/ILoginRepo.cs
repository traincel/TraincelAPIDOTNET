using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Repository.Interface
{
   public interface ILoginRepo
    {
        public Task<bool> AddLoginUser(LoginTable loginUser);
        public Task<bool> UpdateLoginUserStatus(String id, bool isLogin);
        public Task<LoginTable> GetLoginDetails(String emailId);
        public Task<bool> UpdateLoginPassword(ChangePasswordVM changePassword);
        public Task<LoginTable> GetUserLoginDetails(String userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IUserRepo
    {
        public Task<List<UserTable>> GetUsers();
        public Task<UserTable> GetUser(Guid UserId);
        public Task<UserTable> GetUserByEmail(String emailId);
        public Task<bool> AddUsers(UserTable user);
        public Task<bool> DeleteUsers(Guid id);
        public Task<bool> UpdateUser(UserTable userTable);
        public Task<bool> UpdateChangePasswordCode(Guid userId, string code);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface IUserService
    {
        #region UserTable
        public List<UserDetailsVM> GetUsers();
        public UserDetailsVM GetUser(String userId);
        public LoginDetailsVM AddUser(RegisterVM registerVM);
        public UserDetailsVM UpdateUserDetails(RegisterVM userDetails);
        public String ForgotPasswordRequest(String emailId);
        #endregion

        #region LoginTable
        public bool AddLoginUser(LoginRequestVM loginDetailsVM);
        public bool UpdateLoginUserStatus(Guid id, bool isLogin);
        public LoginDetailsVM GetLoginDetails(LoginRequestVM loginCredetials);
        public bool UpdateLoginPassword(ChangePasswordVM changePasswordVM);
        public bool CheckIfUserIsAdmin(AdminLoginRequestVM loginCredetials);

        #endregion

        #region UserCartTable
        public UserCartVM GetUserCartItems(String userId);
        public bool AddItemToUserCart(String userId, List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetailsVM);
        public bool AddItemToCart(Guid userCartId, Guid userId, List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetailsVM);
        public bool DeleteItemsFromCart(String id);
        public int GetNoOfItemsInCart(String userId);
        public int GetUserWebinarCartItem(Guid webinarId, Guid userId);
        #endregion
    }
}

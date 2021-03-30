using AutoMapper;
using Microsoft.OpenApi.Extensions;
using SendGrid.Helpers.Mail;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services.Interface;
using TraincelAPI.Utilities;
using static TraincelAPI.Resources.Enums;

namespace TraincelAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILoginRepo _loginRepo;
        private readonly IUserRepo _userRepo;
        private readonly IUserCartRepo _userCartRepo;
        private readonly ICompaniesService _companiesService;
        private readonly IEmailService _emailService;
        public UserService(IMapper mapper, ILoginRepo loginRepo, IUserRepo userRepo, IUserCartRepo userCartRepo, ICompaniesService companiesService, IEmailService emailService)
        {
            _mapper = mapper;
            _loginRepo = loginRepo;
            _userRepo = userRepo;
            _userCartRepo = userCartRepo;
            _companiesService = companiesService;
            _emailService = emailService;
        }

        #region LoginDetails
        public bool AddLoginUser(LoginRequestVM loginDetailsVM)
        {
            loginDetailsVM.Password = Encryption.Encrypt(loginDetailsVM.Password);
            var loginDetails = _mapper.Map<LoginTable>(loginDetailsVM);
            return _loginRepo.AddLoginUser(loginDetails).Result;
        }


        public bool UpdateLoginUserStatus(Guid id, bool isLogin)
        {
            return _loginRepo.UpdateLoginUserStatus(id.ToString(), isLogin).Result;
        }
        public bool UpdateLoginPassword(ChangePasswordVM changePasswordVM)
        {
            return _loginRepo.UpdateLoginPassword(changePasswordVM).Result;
        }

        public LoginDetailsVM GetLoginDetails(LoginRequestVM loginCredetials)
        {
            var loginDetails = _loginRepo.GetLoginDetails(loginCredetials.EmailId).Result;
            if (loginDetails == null)
            {
                throw new Exception("User not found");
            }

            if (loginDetails.Password == loginCredetials.Password)
            {
                UpdateLoginUserStatus(loginDetails.UserId, true);
                return _mapper.Map<LoginDetailsVM>(loginDetails);
            }
            else
            {
                throw new Exception("Wrong Password");
            }
        }

        public bool CheckIfUserIsAdmin(AdminLoginRequestVM loginCredetials)
        {
            try
            {
                var loginDetails = _loginRepo.GetUserLoginDetails(loginCredetials.UserId).Result;
                return loginDetails.RoleId == (int)Roles.Admin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region UserDetails
        public LoginDetailsVM AddUser(RegisterVM registerVM)
        {
            try
            {
                var user = _userRepo.GetUserByEmail(registerVM.EmailId).Result;
                if (user != null)
                {
                    throw new Exception("User with the same email already exist.");
                }

                if (!registerVM.CompanyId.HasValue && !String.IsNullOrEmpty(registerVM.CompanyName))
                {
                    registerVM.CompanyId = _companiesService.AddCompany(registerVM.CompanyName);
                }
                var userDetails = _mapper.Map<UserTable>(registerVM);
                userDetails.Id = Guid.NewGuid();
                var userResponse = _userRepo.AddUsers(userDetails).Result;
                if (userResponse)
                {
                    var loginDetails = _mapper.Map<LoginTable>(registerVM);
                    loginDetails.UserId = userDetails.Id;
                    loginDetails.UserLocalId = userDetails.LocalId;
                    loginDetails.Id = Guid.NewGuid();
                    loginDetails.RoleId = 1;
                    loginDetails.IsLogIn = true;
                    var loginResponse = _loginRepo.AddLoginUser(loginDetails).Result;
                    if (loginResponse)
                    {
                        //  loginDetails.User = userDetails;
                        return _mapper.Map<LoginDetailsVM>(loginDetails);
                    }
                    else
                    {
                        _userRepo.DeleteUsers(userDetails.Id);
                        throw new Exception("Unable to Register");
                    }
                }
                else
                {
                    throw new Exception("Unable to Register");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserDetailsVM GetUser(String userId)
        {
            var user = _userRepo.GetUser(new Guid(userId)).Result;
            return _mapper.Map<UserDetailsVM>(user);
        }

        public List<UserDetailsVM> GetUsers()
        {
            var user = _userRepo.GetUsers().Result;
            return _mapper.Map<List<UserDetailsVM>>(user);
        }

        public UserDetailsVM UpdateUserDetails(RegisterVM userDetails)
        {
            try
            {
                Company company = new Company();
                if (!userDetails.CompanyId.HasValue && !String.IsNullOrEmpty(userDetails.CompanyName))
                {
                    userDetails.CompanyId = _companiesService.AddCompany(userDetails.CompanyName);
                }
                var userData = _mapper.Map<UserTable>(userDetails);
                userData.CompanyLocalId = company.LocalId;
                var response = _userRepo.UpdateUser(userData).Result;
                return _mapper.Map<UserDetailsVM>(userData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String ForgotPasswordRequest(String emailId)
        {
            try
            {
                Random random = new Random();
                string changePasswordCode = String.Empty;
                for (int i = 0; i < 6; i++)
                {

                    var randomNumber = random.Next(0, 9);
                    changePasswordCode += randomNumber.ToString();

                }
                 var user = _userRepo.GetUserByEmail(emailId).Result;
                if(user != null)
                {
                    var response = _userRepo.UpdateChangePasswordCode(user.Id, changePasswordCode).Result;
                    if (response)
                    {
                        var emailModel = new CommonEmailVM
                        {
                            EmailAddress = emailId,
                            Name = user.FirstName,
                            ChangePasswordCode = changePasswordCode,
                            NotificationType = "forgotpassword"
                        };
                        var emailSent = _emailService.SendEmail(emailModel);
                        if (emailSent)
                        {
                            return user.Id.ToString();
                        }
                        else
                        {
                            throw new Exception("Error in sending the verification code");
                        }
                    }else
                    {
                        throw new Exception("Error in sending the verification code");
                    }
                }              
                else
                {
                    throw new Exception("No user found with the emailId : " + emailId);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        #endregion

        #region UserCart
        public bool AddItemToUserCart(String userId, List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetailsVM)
        {
            try
            {
                var userCart = _userCartRepo.UserCartHasItem(userId).Result;
                if (userCart == null)
                {
                    var newCart = new UserCartMapping
                    {
                        Id = Guid.NewGuid(),
                        UserId = new Guid(userId)
                    };
                    userCart = _userCartRepo.AddNewUserCart(newCart).Result;
                }
                return AddItemToCart(userCart.Id, new Guid(userId), webinarPurchasedOptionsDetailsVM);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserCartVM GetUserCartItems(String userId)
        {
            try
            {
                var userCart = _userCartRepo.GetUserCart(userId).Result;
                return _mapper.Map<UserCartVM>(userCart);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteItemsFromCart(String id)
        {
            try
            {
                return _userCartRepo.DeleteItemsFromCart(id).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNoOfItemsInCart(String userId)
        {
            try
            {
                var count = _userCartRepo.GetNoOfItemsInCart(userId).Result;
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddItemToCart(Guid userCartId, Guid userId, List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetails)
        {
            bool response = false;
            try
            {
                webinarPurchasedOptionsDetails.ForEach((item) =>
                {
                    var userCartItem = _userCartRepo.GetUserCartItem(userId, item.WebinarId).Result;
                    if (userCartItem != null)
                    {
                        userCartItem.Quantity += 1;
                        response = _userCartRepo.UpdateExistingCart(userCartItem).Result;
                    }
                    else
                    {
                        var cartItem = new CartItems
                        {
                            Id = Guid.NewGuid(),
                            CartId = userCartId,
                            WebinarId = item.WebinarId,
                            PurchaseOptionId = item.PurchaseOptionId,
                            Quantity = 1,
                        };
                        response = _userCartRepo.AddNewItemToCart(cartItem).Result;
                    }
                });
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding all items to cart. Please check and try again.");
            }
        }

        public int GetUserWebinarCartItem(Guid webinarId, Guid userId)
        {
            try
            {
                return _userCartRepo.GetUserWebinarCartItems(userId, webinarId).Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

}

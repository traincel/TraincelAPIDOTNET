using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;
using Z.EntityFramework.Plus;

namespace TraincelAPI.Repository
{
    public class UserCartRepo : IUserCartRepo
    {
        private readonly TraincelContext _context;

        public UserCartRepo(TraincelContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteItemsFromCart(String id)
        {
            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(cartItem => cartItem.Id == new Guid(id));
                _context.CartItems.Remove(cartItem);
                var response = await _context.SaveChangesAsync();
                return response >= 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserCartMapping> GetUserCart(String userId)
        {
            try
            {
                var userCart = await _context.UserCartMapping
                    .Include(item => item.CartItems).
                    ThenInclude(cartItem => cartItem.Webinar).
                    ThenInclude(cartItem => cartItem.WebinarPurchasedOptionsDetails)
                    .ThenInclude(webinarPurchaseOptions => webinarPurchaseOptions.PurchaseOption)
                    .FirstOrDefaultAsync(userCart => userCart.UserId == new Guid(userId));
                return userCart;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserCartMapping> UserCartHasItem(String userId)
        {
            return await _context.UserCartMapping.FirstOrDefaultAsync(userCart => userCart.UserId == new Guid(userId));
        }

        public async Task<CartItems> GetUserCartItem(Guid userCartId, Guid itemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(item => item.CartId.Equals(userCartId) && item.WebinarId.Equals(itemId));
        }

        public async Task<bool> AddNewItemToCart(CartItems cartItem)
        {
            try
            {
                cartItem.WebinarLocalId = _context.Webinars.FirstOrDefaultAsync(webinar => webinar.Id == cartItem.WebinarId).Result.LocalId;
                _context.CartItems.Add(cartItem);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } 

        public async Task<bool> UpdateExistingCart(CartItems cartItem)
        {
            var modelToBeUpdated = GetUserCartItem(cartItem.CartId, cartItem.WebinarId).Result;
            modelToBeUpdated.Quantity = cartItem.Quantity;
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

        public async Task<int> GetNoOfItemsInCart(String userId)
        {
            try
            {

                var cart = await _context.UserCartMapping.Include((cart) => cart.CartItems).FirstOrDefaultAsync((cart) => cart.UserId == new Guid(userId));
                return cart == (null) || cart.CartItems == (null) ? 0 : cart.CartItems.Count;
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        public async Task<UserCartMapping> AddNewUserCart(UserCartMapping userCartMapping)
        {
            try
            {
                userCartMapping.UserLocalId = _context.UserTable.FirstOrDefaultAsync((user) => user.Id == userCartMapping.UserId).Result.LocalId;
                _context.UserCartMapping.Add(userCartMapping);
                var response = await _context.SaveChangesAsync();
                return response >= 1 ? userCartMapping : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetUserWebinarCartItems(Guid userId, Guid webinarId)
        {
            try
            {
                var userCartItems = await GetUserCart(userId.ToString());
                var userWebinarCartCount = userCartItems.CartItems.Select(cartItem => cartItem.WebinarId == webinarId).Count();
                return userWebinarCartCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

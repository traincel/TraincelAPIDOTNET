using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IUserCartRepo
    {
        public Task<UserCartMapping> GetUserCart(String userId);
        public Task<bool> DeleteItemsFromCart(String id);
        public Task<UserCartMapping> UserCartHasItem(String userId);
        public Task<CartItems> GetUserCartItem(Guid userCartId, Guid itemId);
        public Task<bool> AddNewItemToCart(CartItems cartItem);
        public Task<bool> UpdateExistingCart(CartItems cartItem);
        public Task<int> GetNoOfItemsInCart(String userId);
        public Task<UserCartMapping> AddNewUserCart(UserCartMapping userCartMapping);
        public Task<int> GetUserWebinarCartItems(Guid userId, Guid webinarId);
    }
}

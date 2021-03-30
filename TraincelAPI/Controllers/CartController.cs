using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;
using static TraincelAPI.Resources.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly IUserService _userService;
        public CartController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<Cart>
        [HttpGet("{userId}")]
        public ResultsVM GetUserCartItems(String userId)
        {
            try
            {
                var cartItems = _userService.GetUserCartItems(userId);
                return new ResultsVM(cartItems, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in getting cart items count");
            }
        }

        // GET api/<Cart>/5
        [HttpGet("{userId}/itemCount")]
        public ResultsVM GetCartItemCount(String userId)
        {
            try
            {
                var cartItems = _userService.GetNoOfItemsInCart(userId);
                return new ResultsVM(cartItems, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, "Error in getting cart items count");
            }
        }

        //POST api/<Cart>
        [HttpPost("{userId}")]
        public ResultsVM AddToCart(String userId, [FromBody] List<WebinarPurchasedOptionsDetailsVM> cartItem)
        {
            try
            {
                var cartItems = _userService.AddItemToUserCart(userId, cartItem);
                return new ResultsVM(cartItems, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, ex.Message);
            }
        }

        // PUT api/<Cart>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Cart>/5
        [HttpDelete("{id}")]
        public ResultsVM Delete(string id)
        {

            try
            {
                var cartItems = _userService.DeleteItemsFromCart(id);
                return new ResultsVM(cartItems, StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, StatusCodes.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{userId}/{webinarId}")]
        public ResultsVM GetUserWebinarCart(Guid userId, Guid webinarId)
        {
            try
            {
                var cartCount = _userService.GetUserWebinarCartItem(webinarId, userId);
                return new ResultsVM(cartCount, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in getting the order");
            }
        }
    }
}

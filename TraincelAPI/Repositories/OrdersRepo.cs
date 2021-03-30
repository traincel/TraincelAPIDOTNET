using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;
using Z.EntityFramework.Plus;

namespace TraincelAPI.Repository
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly TraincelContext _context;

        public OrdersRepo(TraincelContext context)
        {
            _context = context;
        }
        public async Task<int> AddOrders(Orders order)
        {
            var orderItems = order.OrderItems;
            order.UserLocalId = _context.UserTable.FirstOrDefaultAsync(user => user.Id == order.UserId).Result.LocalId;
            order.OrderItems = null;
            _context.Orders.Add(order);
            try
            {
                var insertionCount = 0;
                var response = await _context.SaveChangesAsync();
                if (response >= 1)
                {
                    foreach (OrderItems orderItems1 in orderItems)
                    {
                        orderItems1.PurchaseOption = null;
                        orderItems1.OrderId = order.Id;
                        orderItems1.OrderLocalId = order.LocalId;
                        orderItems1.Webinar = null;
                        _context.OrderItems.Add(orderItems1);
                        var saveChanges = _context.SaveChanges();
                        if (saveChanges >= 1)
                        {
                            var cartItem = await _context.CartItems
                                .Include(cartItem => cartItem.Cart)
                              .FirstOrDefaultAsync(cartItems => cartItems.WebinarId == orderItems1.WebinarId && cartItems.PurchaseOptionId == orderItems1.PurchaseOptionId && cartItems.Cart.UserId == order.UserId);
                            if (cartItem != null)
                            {
                                _context.CartItems.Remove(cartItem);
                                _context.SaveChanges();
                            }
                            insertionCount++;
                        }
                        else
                        {
                            throw new Exception("Error in updating");
                        }
                    }
                }
                return insertionCount == orderItems.Count()? order.LocalId : throw new Exception("Error in updating all the items");
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Orders>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders.Include(order => order.User)                  
                .ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Orders> GetOrder(Guid id)
        {
            try
            {
                var order = await _context.Orders
                    .Where(orders => orders.Id == id)
                    .Include(order => order.User)
                    .Include(order => order.OrderItems)
                    .ThenInclude(orderItems => orderItems.PurchaseOption)
                    .Include(order => order.OrderItems)
                    .ThenInclude(orderItems => orderItems.Webinar)
                    .FirstOrDefaultAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Orders>> GetOrdersOfUser(Guid userId)
        {
            try
            {
                var userOrders = await _context.Orders
                    .Where(orders => orders.UserId == userId).ToListAsync();
                return userOrders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Orders>> GetUserWebinarOrder(Guid userId, Guid webinarId)
        {
            try
            {
                var userOrders = await _context.Orders
                    .Where(orders => orders.UserId == userId)
                    .IncludeFilter(orders => orders.OrderItems.Where(orderItem => orderItem.WebinarId == webinarId)).ToListAsync();
                return userOrders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

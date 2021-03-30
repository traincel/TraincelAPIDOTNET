using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IOrdersRepo
    {
        public Task<List<Orders>> GetOrders();
        public Task<int> AddOrders(Orders categories);
        public Task<Orders> GetOrder(Guid id);
        public Task<List<Orders>> GetOrdersOfUser(Guid userId);
        public Task<List<Orders>> GetUserWebinarOrder(Guid userId, Guid webinarId);
    }
}

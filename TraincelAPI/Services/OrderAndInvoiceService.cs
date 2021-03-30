using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services.Interface;

namespace TraincelAPI.Services
{
    public class OrderAndInvoiceService : IOrdersAndInvoicesService
    {
        private readonly IOrdersRepo _orderRepo;
        private readonly IMapper _mapper;
        public OrderAndInvoiceService(IOrdersRepo ordersRepo, IMapper mapper)
        {
            _orderRepo = ordersRepo;
            _mapper = mapper;
        }

        #region Invoices
        public bool AddInvoice(InvoiceVM invoiceVM)
        {
            throw new NotImplementedException();
        }

        public InvoiceVM GetInvoice(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceVM> GetInvoices()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Orders
        public int AddOrder(OrdersVM orderVM)
        {
            try
            {
                var order = _mapper.Map<Orders>(orderVM);
                order.Id = Guid.NewGuid();
                order.PurchasedDate = DateTime.Now;
                return _orderRepo.AddOrders(order).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdersVM GetOrder(String id)
        {
            try
            {
                var order = _orderRepo.GetOrder(new Guid(id)).Result;
                return _mapper.Map<OrdersVM>(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersVM> GetOrders()
        {
            try
            {
                var order = _orderRepo.GetOrders().Result;
                return _mapper.Map<List<OrdersVM>>(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersVM> GetUsersOrders(String userdId)
        {
            try
            {
                var order = _orderRepo.GetOrdersOfUser(new Guid(userdId)).Result;
                return _mapper.Map<List<OrdersVM>>(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdersVM GetUserWebinarOrder(Guid webinarId, Guid userId)
        {
            try
            {
                var response = _orderRepo.GetUserWebinarOrder(userId, webinarId);
                return _mapper.Map<OrdersVM>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

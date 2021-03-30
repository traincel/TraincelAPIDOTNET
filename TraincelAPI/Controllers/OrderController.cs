using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraincelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrdersAndInvoicesService _ordersAndInvoicesService;
        private readonly IMapper _mapper;
        public OrderController(IOrdersAndInvoicesService ordersAndInvoicesService)
        {
            _ordersAndInvoicesService = ordersAndInvoicesService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public ResultsVM GetOrders()
        {
            try
            {
                var orders = _ordersAndInvoicesService.GetOrders();
                return new ResultsVM(orders, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in getting the orders");
            }
        }

        [HttpGet("user/{userId}")]
        public ResultsVM GetUserOrders(string userId)
        {
            try
            {
                var orders = _ordersAndInvoicesService.GetUsersOrders(userId);
                return new ResultsVM(orders, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in getting user's orders");
            }
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public ResultsVM Get(string id)
        {
            try
            {
                var orders = _ordersAndInvoicesService.GetOrder(id);
                return new ResultsVM(orders, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in getting the order");
            }
        }

        // POST api/<OrderController>
        [HttpPost]
        public ResultsVM Post([FromBody] OrdersVM ordersVM)
        {
            try
            {
                var orders = _ordersAndInvoicesService.AddOrder(ordersVM);
                return new ResultsVM(orders, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in adding the order");
            }
        }

        [HttpGet("{userId}/{webinarId}")]
        public ResultsVM GetUserWebinarOrders(Guid userId, Guid webinarId)
        {
            try
            {
                var orders = _ordersAndInvoicesService.GetUserWebinarOrder(webinarId, userId);
                return new ResultsVM(orders, Resources.Enums.StatusCodes.Success, null);
            }
            catch (Exception ex)
            {
                return new ResultsVM(null, Resources.Enums.StatusCodes.InternalServerError, "Error in getting the order");
            }
        }
    }
}

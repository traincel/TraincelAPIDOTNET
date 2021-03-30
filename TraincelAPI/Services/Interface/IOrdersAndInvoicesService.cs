using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
    public interface IOrdersAndInvoicesService
    {
        #region Orders
        public List<OrdersVM> GetOrders();
        public List<OrdersVM> GetUsersOrders(String userdId);
        public OrdersVM GetOrder(String Id);
        public int AddOrder(OrdersVM orderVM);
        public OrdersVM GetUserWebinarOrder(Guid webinarId, Guid userId);
        #endregion

        #region Invoices
        public List<InvoiceVM> GetInvoices();
        public InvoiceVM GetInvoice(Guid id);
        public bool AddInvoice(InvoiceVM invoiceVM); 
        #endregion
    }
}

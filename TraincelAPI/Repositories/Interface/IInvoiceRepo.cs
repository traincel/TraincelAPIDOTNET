using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IInvoiceRepo
    {
        public Task<List<Invoice>> GetInvoices();
        public Task<Invoice> GetInvoice(Guid id);
        public Task<bool> AddInvoice(Invoice invoice);
    }
}

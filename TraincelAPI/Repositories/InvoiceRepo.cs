using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;

namespace TraincelAPI.Repository
{
    public class InvoiceRepo : IInvoiceRepo
    {
        public Task<bool> AddInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> GetInvoice(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Invoice>> GetInvoices()
        {
            throw new NotImplementedException();
        }
    }
}

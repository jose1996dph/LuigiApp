using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuigiApp.Invoice.Interactors
{
    public interface IInvoiceInteractor
    {
        Task<List<Models.Invoice>> All();
        Task<Models.Invoice> Get(int id);
        Task<bool> Save(Models.Invoice Invoice);
        Task<bool> Delete(Models.Invoice Invoice);
    }
}

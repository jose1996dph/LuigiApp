using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuigiApp.Invoice.Interactors
{
    public interface IInvoiceProductInteractor
    {
        Task<List<Models.InvoiceProduct>> All();
        Task<Models.InvoiceProduct> Get(int id);
        Task<List<Models.InvoiceProduct>> GetByInvoice(int invoiceId);
        Task<bool> Save(Models.InvoiceProduct Invoice);
        Task<bool> Delete(Models.InvoiceProduct Invoice);
    }
}

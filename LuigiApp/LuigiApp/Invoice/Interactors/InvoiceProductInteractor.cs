using LuigiApp.Base.Interactors;
using LuigiApp.Invoice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuigiApp.Invoice.Interactors
{
    public class InvoiceProductInteractor : BaseInteractor<InvoiceProduct>, IInvoiceProductInteractor
    {
        public Task<List<InvoiceProduct>> GetByInvoice(int invoiceId) => DataStore.Select<InvoiceProduct>().Where(x => x.InvoiceId == invoiceId).ToListAsync();
        public override Task<bool> Save(InvoiceProduct invoiceProduct) => DataStore.Insert(invoiceProduct);
    }
}

using LuigiApp.Base.Interactors;
using LuigiApp.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuigiApp.Invoice.Interactors
{
    public class InvoiceInteractor: BaseInteractor<Models.Invoice>, IInvoiceInteractor
    {
        public override async Task<bool> Save(Models.Invoice invoice)
        {
            return await DataStore.Insert(invoice);
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;

namespace LuigiApp.Invoice.Models
{
    public class Invoice
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public Invoice()
        {

        }
        public Invoice(int clientId, List<InvoiceProduct> invoiceProducts, double total)
        {
            ClientId = clientId;
            Total = total;
            Date = DateTime.Now;
        }
    }
}

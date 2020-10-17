using SQLite;

namespace LuigiApp.Invoice.Models
{
    public class InvoiceProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public InvoiceProduct()
        {

        }
        public InvoiceProduct(double quantity, double total, double price, int productId)
        {
            Quantity = quantity;
            Total = total;
            Price = price;
            ProductId = productId;
        }
    }
}

using SQLite;
using Xamarin.Forms;

namespace LuigiApp.Product.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public void Set(string code, string description, double price)
        {
            Code = code;
            Description = description;
            Price = price;
        }
    }
}

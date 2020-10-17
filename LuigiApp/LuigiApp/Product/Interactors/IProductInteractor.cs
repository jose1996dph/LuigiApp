using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuigiApp.Product.Interactors
{
    public interface IProductInteractor
    {
        Task<List<Models.Product>> All();
        Task<Models.Product> Get(int id);
        Task<Models.Product> Get(string code);
        Task<bool> Save(Models.Product product);
        Task<List<Models.Product>> Search(string query);
        Task<bool> Delete(Models.Product product);
    }
}

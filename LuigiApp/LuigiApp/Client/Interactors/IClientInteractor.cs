using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuigiApp.Client.Interactors
{
    public interface IClientInteractor
    {
        Task<List<Models.Client>> All();
        Task<Models.Client> Get(int id);
        Task<Models.Client> Get(string dni);
        Task<bool> Save(Models.Client Client);
        Task<bool> Delete(Models.Client Client);
    }
}

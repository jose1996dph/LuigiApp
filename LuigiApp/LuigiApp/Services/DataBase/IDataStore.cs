using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuigiApp.Services.DataBase
{
    public interface IDataStore
    {
        Task<bool> Execute<T>(string query, params object[] args) where T : new();
        AsyncTableQuery<T> Select<T>() where T : new();
        Task<List<T>> Get<T>() where T : new();
        Task<T> Get<T>(int id) where T : new();
        Task<bool> Insert<T>(T item, bool canLog = true) where T : new();
        Task<bool> Insert<T>(IEnumerable<T> items) where T : new();
        Task<bool> Update<T>(T item) where T : new();
        Task<bool> Update<T>(IEnumerable<T> items) where T : new();
        Task<bool> Delete<T>(object item) where T : new();
        Task<bool> Destroy<T>() where T : new();
    }
}

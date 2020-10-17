using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace LuigiApp.Services.DataBase
{
    public class SQLiteDataBase: IDataStore
    {
        SQLiteAsyncConnection db;
        public SQLiteDataBase()
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LuigiApp.db3");
            db = new SQLiteAsyncConnection(path);
            db.CreateTableAsync<Client.Models.Client>();
            db.CreateTableAsync<Product.Models.Product>();
            db.CreateTableAsync<Invoice.Models.Invoice>();
            db.CreateTableAsync<Invoice.Models.InvoiceProduct>();
        }

        public async Task<bool> Execute<T>(string query, params object[] args) where T : new()
        {
            try
            {
                return (await db.ExecuteAsync(query, args)) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AsyncTableQuery<T> Select<T>() where T : new()
        {
            try
            {
                return db.Table<T>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<T>> Get<T>() where T : new()
        {
            try
            {
                return await db.Table<T>().ToListAsync();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }
        public async Task<T> Get<T>(int id) where T : new()
        {
            try
            {
                return await db.GetAsync<T>(id);
            }
            catch (Exception)
            {
                return new T();
            }
        }
        public async Task<bool> Insert<T>(T item, bool canLog = true) where T : new()
        {
            try
            {
                return (await db.InsertAsync(item)) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Insert<T>(IEnumerable<T> items) where T : new()
        {
            if (items == null)
                return false;

            try
            {
                foreach (var item in items)
                {
                    try
                    {
                        await db.InsertAsync(item);
                    }
                    catch (Exception)
                    {

                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update<T>(T item) where T : new()
        {
            try
            {
                
                return (await db.UpdateAsync(item)) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update<T>(IEnumerable<T> items) where T : new()
        {
            if (items == null)
                return false;

            try
            {
                
                return (await db.UpdateAllAsync(items)) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete<T>(object item) where T : new()
        {
            try
            {
                
                return (await db.DeleteAsync<T>(item)) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Destroy<T>() where T : new()
        {
            try
            {
                
                return (await db.DeleteAllAsync<T>()) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}